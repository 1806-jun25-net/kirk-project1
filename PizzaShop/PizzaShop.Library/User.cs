using System;
using System.Collections.Generic;
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
            //?? majority element??
            //otherwise, use most recent order
            var recommendation = new List<Pizza>();
            recommendation.Add(new Pizza("small"));
            return recommendation;
        }
    }
}
