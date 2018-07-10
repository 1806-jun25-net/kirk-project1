using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Library.Repositories
{
    public class SizingPricingRepository
    {
        private readonly Project1DBContext _db;

        public SizingPricingRepository(Project1DBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<SizingPricing> GetSizingPricing()
        {
            List<SizingPricing> sp = _db.Ingredient.AsNoTracking().ToList();
            return sp;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
