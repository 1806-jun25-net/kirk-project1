using Microsoft.EntityFrameworkCore;
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
            //List<Locations> locations = _db.Locations.AsNoTracking().ToList();
            //List<Locations> locations = _db.Locations.AsNoTracking().Include(m => m.Orders).Include(s => s.LocationIngredientJunction).ToList();
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
                    throw new Exception("Sorting type not recognized.");
            }
            return sortedOrders;
        }

        /*
        public void UpdateLocation(Location loc)
        {
            // calling Update would mark every property as Modified.
            // this way will only mark the changed properties as Modified.
            _db.Entry(_db.Locations.Find(loc.Name)).CurrentValues.SetValues(Mapper.Map(loc));
        }
        */

        public void UpdateLocationInventory(Location loc)
        {
            foreach (Ingredient i in loc.Stock)
                _db.LocationIngredientJunction.Find(loc.Name, i.Name).Quantity = i.Quantity;
                //_db.Entry(_db.LocationIngredientJunction.Find(loc.Name, i.Name)).Entity.Quantity=i.Quantity;
                //_db.Entry(_db.LocationIngredientJunction).State = _db.Entry.EntityState.Modified;
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

            //Disabling ingredient removal per Nick since it may cause conflict with DB

            //4th - check if quantity is now 0
            //if (Stock.First(t => t.Name.Equals(ing.Name)).Quantity == 0)
            //    Stock.Remove(Stock.First(t => t.Name.Equals(ing.Name)));
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
            //4th - check if quantity is now 0, remove if so
            foreach (var item in list)
            {
                if (Stock.First(t => t.Name.Equals(item.Name)).Quantity == 0)
                    Stock.Remove(Stock.First(t => t.Name.Equals(item.Name)));
            }
            return null;
        }




        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
