﻿using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Library.Repositories
{
    public class LocationRepository
    {
        private readonly Project1DBContext _db;

        public LocationRepository(Project1DBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }


        public IEnumerable<Location> GetLocations()
        {
            List<Locations> locations = _db.Locations.Include(m => m.Orders).Include(s => s.LocationIngredientJunction).ThenInclude(k => k.Ingredient).AsNoTracking().ToList();
            return Mapper.Map(locations);
        }

        public Location GetLocationByName(string name)
        {
            Locations loc = _db.Locations.Include(m => m.Orders).Include(s => s.LocationIngredientJunction).ThenInclude(k => k.Ingredient).AsNoTracking().First(l => l.Name.Equals(name));
            return Mapper.Map(loc);
        }

        public bool LocationsContainsName(string name)
        {
            return _db.Locations.AsNoTracking().Any(t => t.Name.Equals(name));
        }


        public IEnumerable<Order> GetSortedOrders(string orderingType, string locName, OrderRepository or)
        {
            //ordering Types:
            // 1= newst first
            // 2= oldest first
            // 3= cheapest first
            // 4= priciest first


            List<Order> sortedOrders = GetLocationByName(locName).OrderHistory.Select(o => or.GetOrderByID(o)).ToList();
            switch (orderingType)
            {
                case "1":
                    sortedOrders = (sortedOrders.OrderByDescending(a => a.Timestamp)).ToList();
                    break;
                case "2":
                    sortedOrders = (sortedOrders.OrderBy(a => a.Timestamp)).ToList();
                    break;
                case "3":
                    sortedOrders = (sortedOrders.OrderBy(a => a.Price)).ToList();
                    break;
                case "4":
                    sortedOrders = (sortedOrders.OrderByDescending(a => a.Price)).ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Sorting type not recognized.");
            }
            return sortedOrders;
        }

        public void UpdateLocationInventory(Location loc)
        {
            foreach (Ingredient i in loc.Stock)
                _db.LocationIngredientJunction.Find(loc.Name, i.Name).Quantity = i.Quantity;
        }

        public void AddStock(Ingredient ing, string locName)
        {
            Location loc = GetLocationByName(locName);
            if (loc.Stock.Any(t => t.Name.Equals(ing.Name)))
                loc.Stock.First(t => t.Name.Equals(ing.Name)).Quantity += ing.Quantity;
            else
            {
                loc.Stock.Add(ing);
                //also register ingredient in ingredient directory
                if (!_db.Ingredients.Contains(Mapper.Map(ing)))
                    _db.Ingredients.Add(Mapper.Map(ing));
            }
            UpdateLocationInventory(loc);
        }

        public void AddBulkStock(List<Ingredient> list, string locName)
        {
            foreach (var item in list)
            {
                AddStock(item, locName);
            }
        }


        /// <summary>
        /// Decreases this Location's quantity of given IIngredient by amount specified in parameter's quantity
        /// If this decrease would put quantity into the negative, leaves quantity unchanged.
        /// If this decrease puts quantity to 0, removes that IIngredient from the stock dictionary
        /// </summary>
        /// <param name="ing"></param>
        /// <returns>null if successful, Name of IIngredient if removal would make quantity negative</returns>
        public string RemoveStock(Ingredient ing, string locName)
        {
            Location loc = GetLocationByName(locName);
            var Stock = loc.Stock;
            //1st - is the ingredient in our dictionary?
            if (!Stock.Any(t => t.Name.Equals(ing.Name)))
                return ing.Name;

            //2nd - does ingredient have sufficient quantity?
            if (Stock.First(t => t.Name.Equals(ing.Name)).Quantity < ing.Quantity)
                return ing.Name;

            //3rd - decrease quantity
            Stock.First(t => t.Name.Equals(ing.Name)).Quantity -= ing.Quantity;

            //No longer removing inventory once it hits 0
            //just in case it could cause refferential integrity issues

            return null;
        }


        /// <summary>
        /// Decreases this Location's quantity of all given IIngredients by amount specified in parameter's quantity for each
        /// If any such decrease would put any quantity into the negative, leaves all quantities unchanged.
        /// </summary>
        /// <param name="list"></param>
        /// <returns>null if successful, Name of first offending IIngredient which would make quantity negative otherwise</returns>
        public string RemoveBulkStock(List<Ingredient> list, string locName)
        {
            Location loc = GetLocationByName(locName);
            var Stock = loc.Stock;
            //1st - Are all ingredients in dictionary?
            //2nd - Do all ingredients have sufficient quantity?
            foreach (var item in list)
            {
                if (!Stock.Any(t => t.Name.Equals(item.Name)) || Stock.First(t => t.Name.Equals(item.Name)).Quantity < item.Quantity)
                    return item.Name;
            }

            //3rd - decrease quantity for all ingredients
            foreach (var item in list)
            {
                Stock.First(t => t.Name.Equals(item.Name)).Quantity -= item.Quantity;
            }

            //No longer removing inventory once it hits 0
            //just in case it could cause refferential integrity issues

            Save();
            return null;
        }

        public string RemoveBulkStockv2(List<Ingredient> list, string locName)
        {
            List<LocationIngredientJunction> lij = _db.LocationIngredientJunction.Where(k => k.LocationId.Equals(locName)).ToList();
            foreach ( Ingredient i in list)
            {
                if (!lij.Any(ing => ing.IngredientId.Equals(i.Name)))
                    return i.Name;
                if (lij.First(ing => ing.IngredientId.Equals(i.Name)).Quantity < i.Quantity)
                    return i.Name;
            }
            foreach (Ingredient i in list)
            {
                lij.First(ing => ing.IngredientId.Equals(i.Name)).Quantity -= i.Quantity;
            }
            
            Save();
            return null;
        }


            public void Save()
        {
            _db.SaveChanges();
        }
    }
}
