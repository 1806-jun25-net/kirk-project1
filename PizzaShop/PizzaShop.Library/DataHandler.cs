using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    [Serializable()]
    public class DataHandler
    {
        public List<Location> Locations { get; set; } = new List<Location>();
        public List<IOrder> Orders { get; set; } = new List<IOrder>();
        public List<IUser> Users { get; set; } = new List<IUser>();

        public IngredientDirectory ingDir = new IngredientDirectory();
        public SizingPricingManager SPM { get; set; } = new SizingPricingManager();

    }
}
