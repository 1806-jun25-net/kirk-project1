using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class Topping : IIngredient
    {
        public string Name { get; }
        public int Quantity { get; set; }

        public Topping(string n, int q) { Name = n; Quantity = q;}
    }
}
