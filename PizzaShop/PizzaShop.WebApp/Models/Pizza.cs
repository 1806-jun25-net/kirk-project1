using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.WebApp.Models
{
    public class Pizza
    {
        public string Size { get; set; } 
        public string CrustType { get; set; }
        public string SauceType { get; set; }
        public HashSet<string> Toppings { get; set; }
        public decimal Price { get; set; }

        public Pizza()
        {
            Toppings = new HashSet<String>();
            Size = null;
            CrustType = "classic crust";
            SauceType = "classic sauce";
            Toppings.Add("cheese");
            Price = 0;
        }
    }
}
