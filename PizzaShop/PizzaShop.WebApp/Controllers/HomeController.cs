using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Library;
using PizzaShop.Library.Models;
using PizzaShop.Library.Repositories;
using PizzaShop.WebApp.Models;

namespace PizzaShop.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public RepositoryHandler RH { get; }

        public HomeController(RepositoryHandler rh)
        {
            RH = rh;
        }

        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //My added actions
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string Username)
        {
            TempData["LastInput"] = Username;
            if (!RH.UserRepo.UsersContainsUsername(Username))
            {
                TempData["FeedbackMsg"] = "Username not recognized.";
                return RedirectToAction(nameof(Index));
            }

            TempData["CurrentUser"] = Username;
            //HttpContext.Session.SetString("CurrentUser", Username);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            TempData.Clear();
            return View();
        }

        public IActionResult CreateNewUser()
        {
            return View(@"..\User\Create");
        }

        public IActionResult OrderStart()
        {
            if (TempData.Peek<Library.OrderBuilder>("OrderBuilder") == null)
            {
                // Redirect to recommended or new order selection
                return View(Models.Mapper.Map(RH.UserRepo.GetRecommendedOrder((string)TempData.Peek("CurrentUser"), RH)).ToList());
            }
            else
            {
                // Resume existing order
                return RedirectToAction(nameof(OrderBuilding));
            }
        }

        public IActionResult OrderBuilding()
        {
            //Grab existing order builder
            OrderBuilder ob = (OrderBuilder)TempData.Peek<Library.OrderBuilder>("OrderBuilder");
            TempData["obFlag"] = "y";
            TempData["pizzaCount"] = ob.GetPizzas().Count;
            //recalculate total cost
            ob.CalculateTotalPrice();
            //return view to print all pizzas w/ options
            return View(Models.Mapper.Map(ob.CurOrder));
        }

        public IActionResult OrderBuildingNew()
        {
            //Create new OrderBuilder
            Library.User curUser = RH.UserRepo.GetUserByUsername((string)TempData.Peek("CurrentUser"));
            //TempData.Put("CurrentOrder", new Library.Order(curUser.Username, curUser.FavStore));
            TempData.Put("OrderBuilder", new Library.OrderBuilder(curUser.Username, curUser.FavStore));
            TempData["obFlag"] = "y";
            //return proceed with OrderBuilding
            return RedirectToAction(nameof(OrderBuilding));
        }

        public IActionResult OrderBuildingRecommended()
        {
            //Create new OrderBuilder
            Library.User curUser = RH.UserRepo.GetUserByUsername((string)TempData.Peek("CurrentUser"));
            //TempData.Put("CurrentOrder", new Library.Order(curUser.Username, curUser.FavStore, RH.UserRepo.GetRecommendedOrder(curUser.Username, RH)));
            TempData.Put("OrderBuilder", new Library.OrderBuilder(curUser.Username, curUser.FavStore, RH.UserRepo.GetRecommendedOrder(curUser.Username, RH)));
            TempData["obFlag"] = "y";
            //return proceed with OrderBuilding
            return RedirectToAction(nameof(OrderBuilding));
        }

        public IActionResult ChangeLocationRedirect()
        {
            List<Models.Location> locs =  Models.Mapper.Map(RH.LocRepo.GetLocations()).ToList();
            TempData["CurrentLocation"] = ((OrderBuilder)TempData.Peek<Library.OrderBuilder>("OrderBuilder")).CurOrder.Store;
            return View(nameof(ChangeLocation), locs);
        }

        [HttpPost]
        public IActionResult ChangeLocation(string newLoc)
        {
            OrderBuilder ob = ((OrderBuilder)TempData.Peek<Library.OrderBuilder>("OrderBuilder"));
            ob.CurOrder.Store = newLoc;
            TempData.Put("OrderBuilder", ob);
            TempData.Remove("CurrentLocation");
            return RedirectToAction(nameof(OrderBuilding));
        }
        
        public IActionResult AddNewPizza()
        {
            List<Models.Ingredient> ingredients = Models.Mapper.Map(RH.IngRepo.GetIngredients()).ToList();
            List<string> sizes = RH.SPRepo.GetSizes().ToList();
            OrderBuilder ob = ((OrderBuilder)TempData.Peek<Library.OrderBuilder>("OrderBuilder"));
            ob.StartNewPizza(sizes.First(), RH);
            TempData.Put("OrderBuilder", ob);
            Models.Pizza currentP = Models.Mapper.Map(ob.ActivePizza);
            PizzaBuilder pb = new PizzaBuilder { P = currentP, Ingredients = ingredients, Sizes = sizes };
            return View(nameof(EditPizza), pb);
        }

        public IActionResult PizzaModding(string button)
        {
            int pn = Int32.Parse(button.Substring(button.LastIndexOf(" ") + 1, button.Length - (button.LastIndexOf(" ") + 1)))-1;
            OrderBuilder ob = (OrderBuilder)TempData.Peek<Library.OrderBuilder>("OrderBuilder");
            Models.Pizza currentP = Models.Mapper.Map(ob.CurOrder.Pizzas[pn]);
            List<Models.Ingredient> ingredients;
            List<string> sizes = RH.SPRepo.GetSizes().ToList();
            PizzaBuilder pb;
            switch (button.Substring(0,6))
            {
                case "Edit T":  //Edit Toppings
                    TempData["EditPizza"] = pn;
                    ingredients = Models.Mapper.Map(RH.IngRepo.GetToppings()).ToList();
                    pb = new PizzaBuilder { P = currentP, Ingredients = ingredients, Sizes = sizes };
                    return View(nameof(EditToppings), pb);
                case "Edit O":  //Edit Other
                    TempData["EditPizza"] = pn;
                    ingredients = Models.Mapper.Map(RH.IngRepo.GetIngredients()).ToList();
                    pb = new PizzaBuilder { P = currentP, Ingredients = ingredients, Sizes=sizes };
                    return View(nameof(EditPizza), pb);
                case "Duplic": //Duplicate
                    if (pn >= 0 && pn < ob.CurOrder.Pizzas.Count)
                        ob.DuplicatePizza(pn);
                    break;
                case "Delete": //Delete
                    if (pn >= 0 && pn < ob.CurOrder.Pizzas.Count)
                        ob.RemovePizza(pn);
                    break;
            }
            TempData.Put("OrderBuilder", ob);
            return RedirectToAction(nameof(OrderBuilding));
        }

        public IActionResult EditPizza(PizzaBuilder pb)
        {
            return View(pb);
        }

        public IActionResult EditToppings(string topping, string button)
        {
            int pn = (int)TempData.Peek("EditPizza");
            OrderBuilder ob = (OrderBuilder)TempData.Peek<Library.OrderBuilder>("OrderBuilder");
            Models.Pizza currentP = Models.Mapper.Map(ob.CurOrder.Pizzas[pn]);
            List<Models.Ingredient> ingredients = Models.Mapper.Map(RH.IngRepo.GetToppings()).ToList();
            List<string> sizes = RH.SPRepo.GetSizes().ToList();
            ob.SwitchActivePizza(pn);
            if (button.Contains("Add"))
            {
                ob.AddToppingToActivePizza(topping, RH);
            }
            else if (button.Contains("Remove"))
            {
                ob.RemoveToppingFromActivePizza(topping, RH);
            }
            ob.ActivePizza.CalculatePrice(RH.SPRepo.GetBasePrice(ob.ActivePizza.Size), RH.SPRepo.GetToppingPrice(ob.ActivePizza.Size));
            ob.CalculateTotalPrice();
            TempData.Put("OrderBuilder", ob);
            PizzaBuilder pb = new PizzaBuilder { P = Models.Mapper.Map(ob.ActivePizza), Ingredients = ingredients, Sizes = sizes };
            return View(pb);
            //return View(nameof(OrderBuilding));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPizza(string Size, string CrustType,
      string SauceType, IEnumerable<bool> Toppings)
        {
            OrderBuilder ob = (OrderBuilder)TempData.Peek<Library.OrderBuilder>("OrderBuilder");
            if (TempData.Peek("EditPizza") != null)
            {
                ob.SwitchActivePizza((int)TempData.Peek("EditPizza"));
                ob.RemovePizza((int)TempData["EditPizza"]);
            }
            ob.ChangeCrustOnActivePizza(CrustType, RH);
            ob.ChangeSauceOnActivePizza(SauceType, RH);
            ob.ChangeSizeOfActivePizza(Size, RH);
            ob.AddActivePizza();
            TempData.Put("OrderBuilder", ob);
            return RedirectToAction(nameof(OrderBuilding));
        }

        public IActionResult FinalizeOrder()
        {
            OrderBuilder ob = (OrderBuilder)TempData.Peek<Library.OrderBuilder>("OrderBuilder");
            string result = ob.FinalizeOrder(RH);
            if (result != null)
            {
                TempData["FeedbackMsg"] = result;
                return RedirectToAction(nameof(OrderBuilding));
            }
            else
            {
                TempData.Remove("OrderBuilder");
                TempData.Remove("obFlag");
                TempData.Remove("pizzaCount");
                return View(Models.Mapper.Map(ob.CurOrder));
            }
        }

        public IActionResult CancelOrder()
        {
            TempData.Remove("OrderBuilder");
            TempData.Remove("obFlag");
            TempData.Remove("pizzaCount");
            TempData["FeedbackMsg"] = "Your order has been cancelled.";
            return View(nameof(Index));
        }

    }
}
