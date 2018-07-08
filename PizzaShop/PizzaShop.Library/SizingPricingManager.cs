using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class SizingPricingManager
    {

        public List<string> Sizes { get; set; } = new List<string>();
        public List<decimal> BasePrices { get; set; } = new List<decimal>();
        public List<decimal> ToppingPrices { get; set; } = new List<decimal>();
        public List<int> IngredientUsageScalar { get; set; } = new List<int>();


        public void AddNewSize(string name, decimal basePrice, decimal toppingPrice, int iUS)
        {
            if (Sizes.Contains(name) || basePrice <= 0m || toppingPrice <= 0m || iUS <= 0)
                return;
            Sizes.Add(name);
            BasePrices.Add(basePrice);
            ToppingPrices.Add(toppingPrice);
            IngredientUsageScalar.Add(iUS);
        }

        public void RemoveSize(string name)
        {
            if (!Sizes.Contains(name))
                return;
            int loc = Sizes.IndexOf(name);
            Sizes.RemoveAt(loc);
            BasePrices.RemoveAt(loc);
            ToppingPrices.RemoveAt(loc);
            IngredientUsageScalar.RemoveAt(loc);
        }

        public decimal GetBasePrice(string name)
        {
            if (!Sizes.Contains(name))
                return -1;
            return BasePrices[Sizes.IndexOf(name)];
        }

        public decimal GetToppingPrice(string name)
        {
            if (!Sizes.Contains(name))
                return -1;
            return ToppingPrices[Sizes.IndexOf(name)];
        }

        public int GetIngredientUsageScalar(string name)
        {
            if (!Sizes.Contains(name))
                return -1;
            return IngredientUsageScalar[Sizes.IndexOf(name)];
        }
    }
}
