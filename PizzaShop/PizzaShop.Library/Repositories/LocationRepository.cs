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

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
