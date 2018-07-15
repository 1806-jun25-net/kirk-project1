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

        public IEnumerable<Order> GetSortedOrders(string orderingType, string user, OrderRepository or)
        {
            //ordering Types:
            // 1= newst first
            // 2= oldest first
            // 3= cheapest first
            // 4= priciest first


            List<Order> sortedOrders = GetUserByUsername(user).OrderHistory.Select (o => or.GetOrderByID(o)).ToList();
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
                    throw new Exception("Sorting type not recognized.");
            }
            return sortedOrders;
        }

        public List<Pizza> GetRecommendedOrder(string user, RepositoryHandler RH)
        {
            //Returns the most recent order, if one exists for that user
            //otherwise, use a basic small pizza
            List<int> oHistory= GetUserByUsername(user).OrderHistory;

            var recommendation = new List<Pizza>();
            if (oHistory.Count > 0)
                recommendation = RH.OrderRepo.GetOrderByID(oHistory[oHistory.Count - 1]).Pizzas;
            else
            {
                SizingPricing sp = RH.SPRepo.GetAllSizingPricing().First();
                recommendation.Add(new Pizza(sp.Size));
                recommendation[0].CalculatePrice((decimal)sp.BasePrice, (decimal)sp.ToppingPrice);
            }
            return recommendation;


            //?? majority element??
        }

        public void UpdateUser(User u)
        {
            if (!UsersContainsUsername(u.Username))
                return;
            Data.Users dUser = _db.Users.Find((u.Username));
            dUser.FirstName = u.FirstName;
            dUser.LastName = u.LastName;
            dUser.Email = u.Email;
            dUser.Phone = u.Phone;
            dUser.FavLocation = u.FavStore;
        }

        public IEnumerable<Library.User> SearchUsers(string searchTerm)
        {
            if (searchTerm == null)
                searchTerm = "";
            List<User> fullNameMatch = GetUsers().Where(u => String.Concat(u.FirstName, " ", u.LastName).ToLower().Contains(searchTerm.ToLower())).ToList();
            List<User> usernameMatch = GetUsers().Where(u => u.Username.ToLower().Contains(searchTerm.ToLower())).ToList();
            var dict = fullNameMatch.ToDictionary(p => p.Username);
            foreach (var x in usernameMatch)
            {
                dict[x.Username] = x;
            }
            return dict.Values.ToList();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
