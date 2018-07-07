using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    [Serializable()]
    public class DataHandler
    {
        public List<Location> Locations { get; set; } = new List<Location>();
        public Dictionary<string, IOrder> Orders { get; set; } = new Dictionary<string, IOrder>();
        public Dictionary<string, IUser> Users { get; set; } = new Dictionary<string, IUser>();

        public IngredientDirectory ingDir = new IngredientDirectory();
        public SizingPricingManager SPM { get; set; } = new SizingPricingManager();

    }
}
