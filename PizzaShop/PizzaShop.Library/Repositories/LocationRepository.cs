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


        public IEnumerable<Locations> GetLocations()
        {
            List<Locations> locations = _db.Locations.AsNoTracking().ToList();
            return locations;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
