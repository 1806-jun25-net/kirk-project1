using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class DataHandler
    {
        public Dictionary<string, Location> Locations { get; set; } = new Dictionary<string, Location>();
        public Dictionary<string, IOrder> Orders { get; set; } = new Dictionary<string, IOrder>();
        public Dictionary<string, IUser> Users { get; set; } = new Dictionary<string, IUser>();

        public IngredientDirectory ingDir = new IngredientDirectory();
        public SizingPricingManager SPM { get; set; } = new SizingPricingManager();
    }
}
