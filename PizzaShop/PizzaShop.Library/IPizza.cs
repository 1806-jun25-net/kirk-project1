using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class IPizza
    {
        public string Size { get; set; }
        public Crust CrustType { get; set; }
        public Sauce SauceType { get; set; }
        public HashSet<Topping> Toppings { get; }
    }
}