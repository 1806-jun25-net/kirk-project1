using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Library;
using PizzaShop.Library.Repositories;
using PizzaShop.WebApp.Models;

namespace PizzaShop.WebApp.Controllers
{
    public class UserController : Controller
    {
        public RepositoryHandler RH { get; }

        public UserController(RepositoryHandler rh)
        {
            RH = rh;
        }
        // GET: User
        public ActionResult Index(List<Models.User> users)
        {
            var libUsers = RH.UserRepo.GetUsers();
            var webUsers = libUsers.Select(x => Models.Mapper.Map(x));
            return View(webUsers);
        }
        


        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            if (RH.UserRepo.UsersContainsUsername(id))
            {
                var libUser = RH.UserRepo.GetUserByUsername(id);
                var webUser = Models.Mapper.Map(libUser);
                return View(webUser);
            }
            else
                return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            Models.User user;
            if (ModelState.IsValid)
            { 
                user = new Models.User
                {
                    Username = collection["Username"],
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    Email = collection["Email"],
                    Phone = collection["Phone"],
                    FavStore = collection["FavStore"]
                };
                RH.UserRepo.AddUser(Models.Mapper.Map(user));
                RH.UserRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
 
        }*/

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.User user)
        {
            if (ModelState.IsValid)
            {
                RH.UserRepo.AddUser(Models.Mapper.Map(user));
                RH.UserRepo.Save();
                TempData["FeedbackMsg"] = "User added";
                return View(@"..\Home\Index");
            }
            TempData["FeedbackMsg"] = "Failed to add user";
            return View(user);
        }


        // GET: User/Edit/5
        public ActionResult Edit(int id, [Bind("Username,FirstName,LastName,Email,Phone,FavLocation")] Models.User u)
        {
            if (!RH.UserRepo.UsersContainsUsername(u.Username))
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                RH.UserRepo.UpdateUser(Models.Mapper.Map(u));

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //My action methods
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchAllUsers(string searchTerm)
        {
            List<Models.User> userList = Models.Mapper.Map(RH.UserRepo.SearchUsers(searchTerm)).ToList();
            TempData["LastInput"] = searchTerm;
            return View(userList);
        }

    }
}