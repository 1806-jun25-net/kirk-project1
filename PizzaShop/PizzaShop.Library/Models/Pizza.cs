using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class Pizza
    {
        public string Size { get; set; }
        public string CrustType { get; set; }
        public string SauceType { get; set; }
        public HashSet<string> Toppings { get; set; }
        public decimal Price { get; set; }
        public Pizza (String s)
        {
            Toppings = new HashSet<String>();
            Size = s;
            CrustType = "classic crust";
            SauceType = "classic sauce";
        }

        public Pizza ()
        {
            Toppings = new HashSet<String>();
            Size = null;
            CrustType = "classic crust";
            SauceType = "classic sauce";
            Price = 0;
        }

        public void AddTopping(string item)
        {
            // Do nothing if topping is already on this pizza
            //Validation that topping is valid must be done elsewhere
            if (Toppings.Contains(item))
                return; 
            Toppings.Add(item);
        }

        public void RemoveTopping(string item)
        {
            // Do nothing if given topping is not currently on pizza
            if (!Toppings.Contains(item))
                return;
            Toppings.Remove(item);
        }

        public void ChangeCrust(string item)
        {
            //Item validation must be done elsewhere where 
            CrustType = item;
        }

        public void ChangesSauce(string item)
        {
            //must validate sauce is valid elsewhere
            SauceType = item;
        }

        public void ChangeSize(string s)
        {
            //Do nothing if given size is not a valid size
            Size = s;
        }

        public decimal CalculatePrice(decimal baseP, decimal topP)
        {
            Price = (decimal)(baseP + topP * Toppings.Count);
            return Price;
        }

    }
}
