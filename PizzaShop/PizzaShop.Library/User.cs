using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Library
{
    public class User
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FavStore { get; set; }
        public List<int> OrderHistory { get; set; } = new List<int>();
        

        public User(string un, string fn, string ln, string em, string ph, string fav)
        {
            Username = un;
            FirstName = fn;
            LastName = ln;
            Email = em;
            Phone = ph;
            FavStore = fav;
        }

        public User() { }

        public List<Pizza> GetRecommendedOrder()
        {
            //Returns the most recent order, if one exists for that user
            //otherwise, use a basic small pizza
            var recommendation = new List<Pizza>();
            if (OrderHistory.Count > 0)
                recommendation = DataAccessor.GetOrderByID(OrderHistory[OrderHistory.Count - 1]).Pizzas;
            else
                recommendation.Add(new Pizza("small"));
            return recommendation;


            //?? majority element??
        }
    }
}
