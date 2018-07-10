using System;
using System.Collections.Generic;

namespace PizzaShop.Data
{
    public partial class LocationIngredientJunction
    {
        public string LocationId { get; set; }
        public int? Quantity { get; set; }
        public string IngredientId { get; set; }

        public Ingredients Ingredient { get; set; }
        public Locations Location { get; set; }
    }
}
