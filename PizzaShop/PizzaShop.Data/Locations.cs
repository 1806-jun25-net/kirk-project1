using System;
using System.Collections.Generic;

namespace PizzaShop.Data
{
    public partial class Locations
    {
        public Locations()
        {
            LocationIngredientJunction = new HashSet<LocationIngredientJunction>();
            Orders = new HashSet<Orders>();
            Users = new HashSet<Users>();
        }

        public string Name { get; set; }

        public ICollection<LocationIngredientJunction> LocationIngredientJunction { get; set; }
        public ICollection<Orders> Orders { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
