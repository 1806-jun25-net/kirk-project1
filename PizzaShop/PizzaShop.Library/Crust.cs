using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class Crust : IIngredient
    {
        public string Name { get; }
        public int Quantity { get; set; }

        public Crust(string n, int q) { Name = n; Quantity = q; }
    }
}
