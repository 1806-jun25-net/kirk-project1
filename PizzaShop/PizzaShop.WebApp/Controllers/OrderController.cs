using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Library;

namespace PizzaShop.WebApp.Controllers
{
    public class OrderController : Controller
    {
        public RepositoryHandler RH { get; }

        public OrderController(RepositoryHandler rh)
        {
            RH = rh;
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        // GET: Order/Details/5
        public ActionResult SingleOrderDetails(int orderId)
        {
            if (RH.OrderRepo.OrdersContainsID(orderId))
            {
                Library.Order o = RH.OrderRepo.GetOrderByID(orderId);
                return View(Models.Mapper.Map(o));
            }
            TempData["FeedbackMsg"] = $"Could not find order ID {orderId}";
            return View(nameof(Index));
        }

        // GET: Order/Details/5
        public ActionResult AllOrderDetails(string sortingType)
        {
            //ordering Types:
            // 1= newst first
            // 2= oldest first
            // 3= cheapest first
            // 4= priciest first
            switch (sortingType)
            {
                case "1":
                    TempData["FeedbackMsg"] = "Sorting orders by earliest date first:";
                    break;
                case "2":
                    TempData["FeedbackMsg"] = "Sorting orders by oldest date first:";
                    break;
                case "3":
                    TempData["FeedbackMsg"] = "Sorting orders by price ascending:";
                    break;
                case "4":
                    TempData["FeedbackMsg"] = "Sorting orders by price descending:";
                    break;
                default:
                    TempData["FeedbackMsg"] = $"Error in radio buttons.  Value of {sortingType}";
                    return View(nameof(Index));
            }
            List<Library.Order> orderList = RH.OrderRepo.GetSortedOrders(sortingType).ToList();
            if (orderList.Count ==0)
            {
                TempData["FeedbackMsg"] = $"No orders found";
                return View(nameof(Index));
            }
            return View(Models.Mapper.Map(orderList));
        }

        // GET: Order/Details/5
        public ActionResult UserOrderDetails(string Username, string sortingType)
        {
            //ordering Types:
            // 1= newst first
            // 2= oldest first
            // 3= cheapest first
            // 4= priciest first
            if (!RH.UserRepo.UsersContainsUsername(Username))
            {
                TempData["FeedbackMsg"] = "Username not recognized.";
                return RedirectToAction(nameof(Index));
            }

            switch (sortingType)
            {
                case "1":
                    TempData["FeedbackMsg"] = $"Sorting orders placed by {Username} by earliest date first:";
                    break;
                case "2":
                    TempData["FeedbackMsg"] = $"Sorting orders placed by {Username} by oldest date first:";
                    break;
                case "3":
                    TempData["FeedbackMsg"] = $"Sorting orders placed by {Username} by price ascending:";
                    break;
                case "4":
                    TempData["FeedbackMsg"] = $"Sorting orders placed by {Username} by price descending:";
                    break;
                default:
                    TempData["FeedbackMsg"] = $"Error in radio buttons.  Value of {sortingType}";
                    return View(nameof(Index));
            }
            List<Library.Order> orderList = RH.UserRepo.GetSortedOrders(sortingType, Username, RH.OrderRepo).ToList();
            if (orderList.Count == 0)
            {
                TempData["FeedbackMsg"] = $"No orders found";
                return View(nameof(Index));
            }
            return View(nameof(AllOrderDetails), Models.Mapper.Map(orderList));
        }

        // GET: Order/Details/5
        public ActionResult LocationOrderDetails(string locationName, string sortingType)
        {
            //ordering Types:
            // 1= newst first
            // 2= oldest first
            // 3= cheapest first
            // 4= priciest first
            if (!RH.LocRepo.LocationsContainsName(locationName))
            {
                TempData["FeedbackMsg"] = "Location not recognized.";
                return RedirectToAction(nameof(Index));
            }

            switch (sortingType)
            {
                case "1":
                    TempData["FeedbackMsg"] = $"Sorting {locationName} orders by earliest date first:";
                    break;
                case "2":
                    TempData["FeedbackMsg"] = $"Sorting {locationName} orders by oldest date first:";
                    break;
                case "3":
                    TempData["FeedbackMsg"] = $"Sorting {locationName} orders by price ascending:";
                    break;
                case "4":
                    TempData["FeedbackMsg"] = $"Sorting {locationName} orders by price descending:";
                    break;
                default:
                    TempData["FeedbackMsg"] = $"Error in radio buttons.  Value of {sortingType}";
                    return View(nameof(Index));
            }
            List<Library.Order> orderList = RH.LocRepo.GetSortedOrders(sortingType, locationName, RH.OrderRepo).ToList();
            if (orderList.Count == 0)
            {
                TempData["FeedbackMsg"] = $"No orders found";
                return View(nameof(Index));
            }
            return View(nameof(AllOrderDetails), Models.Mapper.Map(orderList));
        }

    }
}