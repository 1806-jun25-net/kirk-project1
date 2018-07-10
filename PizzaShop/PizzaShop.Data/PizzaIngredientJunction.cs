using System;
using System.Collections.Generic;

namespace PizzaShop.Data
{
    public partial class PizzaIngredientJunction
    {
        public int PizzaId { get; set; }
        public string IngredientId { get; set; }

        public Ingredients Ingredient { get; set; }
        public Pizzas Pizza { get; set; }
    }
}
