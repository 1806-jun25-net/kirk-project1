﻿using System;
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
