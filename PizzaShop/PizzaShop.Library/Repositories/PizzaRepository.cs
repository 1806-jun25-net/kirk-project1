using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Library.Repositories
{
    public class PizzaRepository
    {
        private readonly Project1DBContext _db;

        public PizzaRepository(Project1DBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<Pizzas> GetSizingPricing()
        {
            List<Pizzas> sp = _db.Pizzas.Include(p => p.PizzaIngredientJunction).AsNoTracking().ToList();
            return sp;
        }

        public void AddPizza(Pizza p)
        {
            _db.Add(Mapper.Map(p));
        }

        public void AddPizza(Data.Pizzas p)
        {
            _db.Add(p);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
