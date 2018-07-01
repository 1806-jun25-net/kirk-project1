using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class SizingPricingManager
    {
        public HashSet<string> Sizes { get; } = new HashSet<string>();
        private Dictionary<string, decimal> basePrices = new Dictionary<string, decimal>();
        private Dictionary<string, decimal> toppingPrices = new Dictionary<string, decimal>();

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

        public decimal getBasePrice(string name)
        {
            if (!basePrices.ContainsKey(name))
                return -1;
            return basePrices[name];
        }

        public decimal getToppingPrice(string name)
        {
            if (!toppingPrices.ContainsKey(name))
                return -1;
            return toppingPrices[name];
        }
    }
}
