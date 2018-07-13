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

        public IEnumerable<SizingPricing> GetAllSizingPricing()
        {
            List<SizingPricing> sp = _db.Ingredient.AsNoTracking().ToList();
            return sp.OrderBy(i => i.IngredientUsageScalar);
        }

        public IEnumerable<string> GetSizes()
        {
            return GetAllSizingPricing().Select(p => p.Size);
        }

        public void AddNewSize(string name, decimal basePrice, decimal toppingPrice, int iUS)
        {
            if (ContainsSize(name) || basePrice <= 0m || toppingPrice <= 0m || iUS <= 0)
                return;
            var sp = new Data.SizingPricing
            {
                Size = name,
                BasePrice = basePrice,
                ToppingPrice = toppingPrice,
                IngredientUsageScalar = iUS
            };
            _db.Add(sp);
        }

        public bool ContainsSize(string s)
        {
            return _db.Ingredient.AsNoTracking().Any(z => z.Size.Equals(s));
        }

        public SizingPricing GetSizingPricingBySize(string s)
        {
            return _db.Ingredient.AsNoTracking().First(z => z.Size.Equals(s));
        }

        public decimal GetBasePrice(string name)
        {
            if (!ContainsSize(name))
                throw new Exception("Given size does not exist in DB");
            return (decimal)_db.Ingredient.AsNoTracking().First(i => i.Size.Equals(name)).BasePrice;
        }

        public decimal GetToppingPrice(string name)
        {
            if (!ContainsSize(name))
                throw new Exception("Given size does not exist in DB");
            return (decimal)_db.Ingredient.AsNoTracking().First(i => i.Size.Equals(name)).ToppingPrice;
        }

        public int GetIngredientUsageScalar(string name)
        {
            if (!ContainsSize(name))
                throw new Exception("Given size does not exist in DB");
            return (int)_db.Ingredient.AsNoTracking().First(i => i.Size.Equals(name)).IngredientUsageScalar;
        }

        public void RemoveSize(string name)
        {
            if (!ContainsSize(name))
                throw new Exception("Given size does not exist in DB");
            _db.Remove(GetSizingPricingBySize(name));
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
