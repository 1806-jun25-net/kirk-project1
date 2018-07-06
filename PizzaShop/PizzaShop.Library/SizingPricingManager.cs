using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class SizingPricingManager
    {
        public List<string> Sizes { get; } = new List<string>() { "small", "medium", "large", "party" };
        private Dictionary<string, decimal> basePrices = new Dictionary<string, decimal>() { { "small", 5 }, {"medium", 7.5m}, { "large", 10 }, { "party", 40 } };
        private Dictionary<string, decimal> toppingPrices = new Dictionary<string, decimal>() { { "small", .5m }, { "medium", .75m }, { "large", 1m }, { "party", 4m } };
        private Dictionary<string, int> ingredientUsageScalar = new Dictionary<string, int>() { { "small", 1 }, { "medium", 2 }, { "large", 3 }, { "party", 4 } };


        public void AddNewSize(string name, decimal basePrice, decimal toppingPrice)
        {
            if (Sizes.Contains(name) || basePrice <= 0m || toppingPrice <= 0m)
                return;
            Sizes.Add(name);
            basePrices.Add(name, basePrice);
            toppingPrices.Add(name, toppingPrice);
        }

        public void RemoveSize(string name)
        {
            if (!Sizes.Contains(name))
                return;
            Sizes.Remove(name);
            basePrices.Remove(name);
            toppingPrices.Remove(name);
        }

        public decimal GetBasePrice(string name)
        {
            if (!basePrices.ContainsKey(name))
                return -1;
            return basePrices[name];
        }

        public decimal GetToppingPrice(string name)
        {
            if (!toppingPrices.ContainsKey(name))
                return -1;
            return toppingPrices[name];
        }

        public int GetIngredientUsageScalar(string name)
        {
            if (!toppingPrices.ContainsKey(name))
                return -1;
            return ingredientUsageScalar[name];
        }
    }
}
