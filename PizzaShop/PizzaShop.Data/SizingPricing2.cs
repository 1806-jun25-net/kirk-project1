using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Data
{
    public partial class SizingPricing
    {
        public SizingPricing(string size, decimal baseP, decimal topP, int scalar)
        {
            Pizzas = new HashSet<Pizzas>();
            Size = size;
            BasePrice = baseP;
            ToppingPrice = topP;
            IngredientUsageScalar = scalar;
        }
    }
}
