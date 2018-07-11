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

        public IEnumerable<Ingredients> GetIngredients()
        {
            List<Ingredients> ing = _db.Ingredients.AsNoTracking().ToList();
            return ing;
        }

        public void AddIngredient(Ingredient i)
        {
            _db.Add(Mapper.Map(i));
        }

        public void AddIngredient(Data.Ingredients i)
        {
            _db.Add(i);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
