﻿using Microsoft.EntityFrameworkCore;
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



        public IEnumerable<Order> GetOrders()
        {
            List<Orders> orders = _db.Orders.Include( o => o.OrderPizzaJunction).ThenInclude( p => p.Pizza).ThenInclude( j => j.PizzaIngredientJunction).ThenInclude( k => k.Ingredient).AsNoTracking().ToList();
            return Mapper.Map(orders);
        }

        public IEnumerable<Order> GetSortedOrders(string orderingType)
        {
            //ordering Types:
            // 1= newst first
            // 2= oldest first
            // 3= cheapest first
            // 4= priciest first

            List<Orders> sortedOrders = _db.Orders.Include(o => o.OrderPizzaJunction).ThenInclude(p => p.Pizza).ThenInclude(j => j.PizzaIngredientJunction).ThenInclude(k => k.Ingredient).AsNoTracking().ToList();
            switch (orderingType)
            {
                case "1":
                    sortedOrders = (sortedOrders.OrderByDescending(a => a.Timestamp)).ToList();
                    break;
                case "2":
                    sortedOrders = (sortedOrders.OrderBy(a => a.Timestamp)).ToList();
                    break;
                case "3":
                    sortedOrders = (sortedOrders.OrderBy(a => a.Price)).ToList();
                    break;
                case "4":
                    sortedOrders = (sortedOrders.OrderByDescending(a => a.Price)).ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Sorting type not recognized.");
            }
            return Mapper.Map(sortedOrders);
        }

        public Order GetOrderByID(int id)
        {
            Orders ord = _db.Orders.Include(o => o.OrderPizzaJunction).ThenInclude(p => p.Pizza).ThenInclude(j => j.PizzaIngredientJunction).ThenInclude(k => k.Ingredient).AsNoTracking().First(o => o.Id == id);
            return Mapper.Map(ord);
        }

        public bool OrdersContainsID(int id)
        {
            return _db.Orders.AsNoTracking().Any(t => t.Id == id);
        }

        public void AddOrder(Order order)
        {
            //take care of order basics
            _db.Add(Mapper.Map(order));
            //take care of individual pizzas
            foreach (var p in order.Pizzas)
            {
                Data.Pizzas datP = Mapper.Map(p);
                _db.Add(datP);
                Save();
                OrderPizzaJunction opj = new OrderPizzaJunction { OrderId = order.Id, Quantity = 1, PizzaId = datP.Id };
                AddOrderPizzaJunction(opj);
                //add crust to ingredients
                Data.Ingredients datI = Mapper.Map(new Ingredient { Name = p.CrustType, Type = "crust" });
                AddPizzaIngredientJunction(new PizzaIngredientJunction { PizzaId = datP.Id, IngredientId = datI.Name });
                //add sauce to ingredients
                datI = Mapper.Map(new Ingredient { Name = p.SauceType, Type = "sauce" });
                AddPizzaIngredientJunction(new PizzaIngredientJunction { PizzaId = datP.Id, IngredientId = datI.Name });

                //add remaining toppings to ingredients
                foreach (var t in p.Toppings)
                {
                    datI = Mapper.Map(new Ingredient { Name = t, Type = "topping" });
                    AddPizzaIngredientJunction(new PizzaIngredientJunction { PizzaId = datP.Id, IngredientId = datI.Name });
                }
                Save();
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
