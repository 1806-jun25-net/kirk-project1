using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Library.Repositories
{
    public class OrderRepository
    {
        private readonly Project1DBContext _db;

        public OrderRepository(Project1DBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }



        public IEnumerable<Orders> GetOrders()
        {
            List<Orders> orders = _db.Orders.Include( o => o.OrderPizzaJunction).ThenInclude( p => p.Pizza).ThenInclude( j => j.PizzaIngredientJunction).ThenInclude( k => k.Ingredient).AsNoTracking().ToList();
            return orders;
        }

        public void AddOrder(Order order)
        {
            //take care of order basics
            _db.Add(Mapper.Map(order));
            //take care of individual pizzas
            foreach (var p in order.Pizzas)
            {
                Data.Pizzas datP = Mapper.Map(p);
                DataAccessor.DH.PizzaRepo.AddPizza(datP);
                Save();
                OrderPizzaJunction opj = new OrderPizzaJunction { OrderId = order.Id, Quantity = 1, PizzaId = datP.Id };
                AddOrderPizzaJunction(opj);
                //add crust to ingredients
                Data.Ingredients datI = Mapper.Map(new Ingredient { Name = p.CrustType, Type = "crust" });
                if (!DataAccessor.DH.IngRepo.GetIngredients().Any(i => i.Name.Equals(datI.Name)))
                {
                    DataAccessor.DH.IngRepo.AddIngredient(datI);
                }
                AddPizzaIngredientJunction(new PizzaIngredientJunction { PizzaId = datP.Id, IngredientId = datI.Name });
                //add sauce to ingredients
                datI = Mapper.Map(new Ingredient { Name = p.SauceType, Type = "sauce" });
                if (!DataAccessor.DH.IngRepo.GetIngredients().Any(i => i.Name.Equals(datI.Name)))
                {
                    DataAccessor.DH.IngRepo.AddIngredient(datI);
                }
                AddPizzaIngredientJunction(new PizzaIngredientJunction { PizzaId = datP.Id, IngredientId = datI.Name });

                //add remaining toppings to ingredients
                foreach (var t in p.Toppings)
                {
                    datI = Mapper.Map(new Ingredient { Name = t, Type = "topping" });
                    if (!DataAccessor.DH.IngRepo.GetIngredients().Any(i => i.Name.Equals(datI.Name)))
                    {
                        DataAccessor.DH.IngRepo.AddIngredient(datI);
                    }
                    AddPizzaIngredientJunction(new PizzaIngredientJunction { PizzaId = datP.Id, IngredientId = datI.Name });
                }
            }
        }

        public void AddOrderPizzaJunction(OrderPizzaJunction opj)
        {
            _db.Add(opj);
        }

        public void AddPizzaIngredientJunction(PizzaIngredientJunction pij)
        {
            _db.Add(pij);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
