//Functionality Still Required:
//Order history
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Library
{
    public class Location
    {
        public List<Ingredient> Stock { get; set; } = new List<Ingredient>();
        public List<int> OrderHistory { get; set; } = new List<int>();
        public String Name { get; set; }

        public Location(String name)
        {
            Name = name;
            Stock = new List<Ingredient>();
            OrderHistory = new List<int>();
        }

        public Location()
        {
            Stock = new List<Ingredient>();
            OrderHistory = new List<int>();
            Name = "";
        }

    }
}
