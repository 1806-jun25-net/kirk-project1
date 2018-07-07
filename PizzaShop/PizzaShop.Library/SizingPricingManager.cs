using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class SizingPricingManager
    {
        public List<string> Sizes { get; } = new List<string>() { "small", "medium", "large", "party" };
        private List<decimal> basePrices = new List<decimal>() {  5m, 7.5m, 10m, 40m };
        private List<decimal> toppingPrices = new List<decimal>() { .5m, .75m, 1m, 4m };
        private List<int> ingredientUsageScalar = new List<int>() { 1, 2, 3, 4 };


        public void AddNewSize(string name, decimal basePrice, decimal toppingPrice)
        {
            if (Sizes.Contains(name) || basePrice <= 0m || toppingPrice <= 0m)
                return;
            Sizes.Add(name);
            basePrices.Add(basePrice);
            toppingPrices.Add(toppingPrice);
        }

        public void RemoveSize(string name)
        {
            if (!Sizes.Contains(name))
                return;
            int loc = Sizes.IndexOf(name);
            Sizes.RemoveAt(loc);
            basePrices.RemoveAt(loc);
            toppingPrices.RemoveAt(loc);
        }

        public decimal GetBasePrice(string name)
        {
            if (!Sizes.Contains(name))
                return -1;
            return basePrices[Sizes.IndexOf(name)];
        }

        public decimal GetToppingPrice(string name)
        {
            if (!Sizes.Contains(name))
                return -1;
            return toppingPrices[Sizes.IndexOf(name)];
        }

        public int GetIngredientUsageScalar(string name)
        {
            if (!Sizes.Contains(name))
                return -1;
            return ingredientUsageScalar[Sizes.IndexOf(name)];
        }
    }
}
