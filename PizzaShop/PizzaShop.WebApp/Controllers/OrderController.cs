using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PizzaShop.WebApp.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        // GET: Order/Details/5
        public ActionResult SingleOrderDetails(int orderId)
        {
            
            return View();
        }

        // GET: Order/Details/5
        public ActionResult AllOrderDetails(int sortingType)
        {
            
            return View();
        }

    }
}