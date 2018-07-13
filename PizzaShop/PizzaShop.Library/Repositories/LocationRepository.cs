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
            return _db.Locations.Include(m => m.Orders).Include(s => s.LocationIngredientJunction).ThenInclude(k => k.Ingredient).AsNoTracking().Any(t => t.Name.Equals(name));
        }


        public IEnumerable<Order> GetSortedOrders(int orderingType, string locName, OrderRepository or)
        {
            //ordering Types:
            // 1= newst first
            // 2= oldest first
            // 3= cheapest first
            // 4= priciest first


            List<Order> sortedOrders = GetLocationByName(locName).OrderHistory.Select(o => or.GetOrderByID(o)).ToList();
            switch (orderingType)
            {
                case 1:
                    sortedOrders = (sortedOrders.OrderByDescending(a => a.Timestamp)).ToList();
                    break;
                case 2:
                    sortedOrders = (sortedOrders.OrderBy(a => a.Timestamp)).ToList();
                    break;
                case 3:
                    sortedOrders = (sortedOrders.OrderBy(a => a.Price)).ToList();
                    break;
                case 4:
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

        public void AddStock(Ingredient ing, string name)
        {
            Location loc = GetLocationByName(name);
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

        public void AddBulkStock(List<Ingredient> list, string name)
        {
            foreach (var item in list)
            {
                AddStock(item, name);
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
