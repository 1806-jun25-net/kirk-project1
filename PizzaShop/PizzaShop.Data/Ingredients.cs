using System;
using System.Collections.Generic;

namespace PizzaShop.Data
{
    public partial class Ingredients
    {
        public Ingredients()
        {
            LocationIngredientJunction = new HashSet<LocationIngredientJunction>();
            PizzaIngredientJunction = new HashSet<PizzaIngredientJunction>();
        }

        public string Name { get; set; }
        public string Type { get; set; }

        public ICollection<LocationIngredientJunction> LocationIngredientJunction { get; set; }
        public ICollection<PizzaIngredientJunction> PizzaIngredientJunction { get; set; }
    }
}
