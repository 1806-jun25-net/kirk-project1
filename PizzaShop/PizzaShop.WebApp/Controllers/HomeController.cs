using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Library;
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

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
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
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            TempData["CurrentUser"] = null;
            TempData["LastInput"] = null;
            return View();
        }

        public IActionResult StartOrder()
        {
            if (TempData["OrderBuilder"] == null)
            {

            }
            return View();
        }

        public IActionResult Create()
        {
            return View(@"..\User\Create");
        }
    }
}
