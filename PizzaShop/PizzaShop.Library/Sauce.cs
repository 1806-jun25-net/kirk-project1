using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    class Sauce : IIngredient
    {
        public string Name { get; }
        public int Quantity { get; set; }
    }
}
