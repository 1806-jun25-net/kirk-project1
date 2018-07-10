using System;
using System.Collections.Generic;

namespace PizzaShop.Data
{
    public partial class SizingPricing
    {
        public SizingPricing()
        {
            Pizzas = new HashSet<Pizzas>();
        }

        public string Size { get; set; }
        public decimal? BasePrice { get; set; }
        public decimal? ToppingPrice { get; set; }
        public int? IngredientUsageScalar { get; set; }

        public ICollection<Pizzas> Pizzas { get; set; }
    }
}
