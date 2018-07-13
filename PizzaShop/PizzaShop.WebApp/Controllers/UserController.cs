﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Library.Repositories;
using PizzaShop.WebApp.Models;

namespace PizzaShop.WebApp.Controllers
{
    public class UserController : Controller
    {
        public UserRepository URepo { get; }

        public UserController(UserRepository UR)
        {
            URepo = UR;
        }
        // GET: User
        public ActionResult Index()
        {
            var libUsers = URepo.GetUsers();
            var webUsers = libUsers.Select(x => Mapper.Map(x));
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            var libUser = URepo.GetUserByUsername(id);
            var webUser = Mapper.Map(libUser);
            return View(webUser);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}