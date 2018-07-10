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


        public IEnumerable<Locations> GetLocations()
        {
            //List<Locations> locations = _db.Locations.AsNoTracking().ToList();
            List<Locations> locations = _db.Locations.AsNoTracking().Include(m => m.Orders).Include(s => s.LocationIngredientJunction).ToList();
            //List<Locations> locations = _db.Locations.AsNoTracking().Include(m => m.Orders).Include(s => s.LocationIngredientJunction.AsQueryable().Include(k => k.Ingredient )).ToList();
            return locations;
        }

        public void UpdateLocation(Location loc)
        {
            // calling Update would mark every property as Modified.
            // this way will only mark the changed properties as Modified.
            _db.Entry(_db.Locations.Find(loc.Name)).CurrentValues.SetValues(Mapper.Map(loc));
        }

        public void UpdateLocationInventory(Location loc)
        {
            foreach (Ingredient i in loc.Stock)
            _db.Entry(_db.LocationIngredientJunction.Find(loc.Name, i.Name)).CurrentValues.SetValues(i);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
