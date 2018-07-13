using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.WebApp.Models
{
    public class Location
    {
        public List<Ingredient> Stock { get; set; } = new List<Ingredient>();
        public String Name { get; set; }
    }
}
