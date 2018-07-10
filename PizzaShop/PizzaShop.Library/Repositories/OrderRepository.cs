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
            List<Orders> orders = _db.Orders.AsNoTracking().ToList();
            return orders;
        }

        public void AddOrder(Order order)
        {
            _db.Add(Mapper.Map(order));
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
