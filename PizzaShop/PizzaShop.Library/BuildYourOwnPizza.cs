using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class BuildYourOwnPizza : IPizza
    {
        public string Size { get; set; }
        public string CrustType { get; set; }
        public string SauceType { get; set; }
        public HashSet<string> Toppings { get; set; }
        public decimal Price { get; set; }
        public BuildYourOwnPizza (String s)
        {
            Toppings = new HashSet<String>();
            Size = s;
            CrustType = "classic crust";
            SauceType = "classic sauce";
            Toppings.Add("cheese");
            Price = DataAccessor.DH.SPM.GetBasePrice(s) + DataAccessor.DH.SPM.GetToppingPrice(s);
        }

        public void AddTopping(string item)
        {
            // Do nothing if topping is already on this pizza, or is not a valid known topping
            if (Toppings.Contains(item) || !DataAccessor.DH.ingDir.Toppings.Contains(item))
                return;
            Toppings.Add(item);
            Price += DataAccessor.DH.SPM.GetToppingPrice(Size);
        }

        public void RemoveTopping(string item)
        {
            // Do nothing if given topping is not currently on pizza
            if (!Toppings.Contains(item))
                return;
            Toppings.Remove(item);
            Price -= DataAccessor.DH.SPM.GetToppingPrice(Size);
        }

        public void ChangeCrust(string item)
        {
            //Do nothing if given crust is not a valid crust
            if (!DataAccessor.DH.ingDir.Crusts.Contains(item))
                return;
            CrustType = item;
        }

        public void ChangesSauce(string item)
        {
            //Do nothing if given sauce is not a valid sauce
            if (!DataAccessor.DH.ingDir.Sauces.Contains(item))
                return;
            SauceType = item;
        }

        public void changeSize(string s)
        {
            //Do nothing if given size is not a valid size
            if (!DataAccessor.DH.SPM.Sizes.Contains(s))
                return;
            Size = s;
            Price = DataAccessor.DH.SPM.GetBasePrice(s) + DataAccessor.DH.SPM.GetToppingPrice(s) * Toppings.Count;
        }

    }
}
