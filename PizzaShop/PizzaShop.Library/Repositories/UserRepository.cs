using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Library.Repositories
{
    public class UserRepository
    {
        private readonly Project1DBContext _db;

        public UserRepository(Project1DBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<User> GetUsers()
        {
            //List<Users> users = _db.Users.AsNoTracking().ToList();
            List<Users> users = _db.Users.AsNoTracking().Include(m => m.Orders).ToList();
            return Mapper.Map(users);
        }

        public User GetUserByUsername(string username)
        {
            Users u = _db.Users.AsNoTracking().Include(m => m.Orders).First(t => t.Username.Equals(username));
            return Mapper.Map(u);
        }

        public bool UsersContainsUsername(string username)
        {
            return _db.Users.AsNoTracking().Any(t => t.Username.Equals(username));
        }

        public void AddUser(User u)
        {
            _db.Add(Mapper.Map(u));
        }

        public IEnumerable<Order> GetSortedOrders(int orderingType, string user, OrderRepository or)
        {
            //ordering Types:
            // 1= newst first
            // 2= oldest first
            // 3= cheapest first
            // 4= priciest first


            List<Order> sortedOrders = GetUserByUsername(user).OrderHistory.Select (o => or.GetOrderByID(o)).ToList();
            switch (orderingType)
            {
                case 1:
                    sortedOrders = (sortedOrders.OrderByDescending(a => a.Timestamp)).ToList();
                    break;
                case 2:
                    sortedOrders = (sortedOrders.OrderBy(a => a.Timestamp)).ToList();
                    break;
                case 3:
                    sortedOrders = (sortedOrders.OrderBy(a => a.Price)).ToList();
                    break;
                case 4:
                    sortedOrders = (sortedOrders.OrderByDescending(a => a.Price)).ToList();
                    break;
                default:
                    throw new Exception("Sorting type not recognized.");
            }
            return sortedOrders;
        }

        public List<Pizza> GetRecommendedOrder(string user, OrderRepository or)
        {
            //Returns the most recent order, if one exists for that user
            //otherwise, use a basic small pizza
            List<int> oHistory= GetUserByUsername(user).OrderHistory;

            var recommendation = new List<Pizza>();
            if (oHistory.Count > 0)
                recommendation = or.GetOrderByID(oHistory[oHistory.Count - 1]).Pizzas;
            else
                recommendation.Add(new Pizza("small"));
            return recommendation;


            //?? majority element??
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
