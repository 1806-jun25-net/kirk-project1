using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    class Topping : IIngredient
    {
        public string Name { get; }
        public int Quantity { get; set; }
    }
}
