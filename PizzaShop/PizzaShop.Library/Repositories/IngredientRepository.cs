using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Library.Repositories
{
    public class IngredientRepository
    {
        private readonly Project1DBContext _db;

        public IngredientRepository(Project1DBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<Ingredients> GetIngredient()
        {
            List<Ingredients> ing = _db.Ingredients.AsNoTracking().ToList();
            //List<Ingredients> ing = _db.Ingredients.AsNoTracking().Include(m => m.LocationIngredientJunction).ToList();
            return ing;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
