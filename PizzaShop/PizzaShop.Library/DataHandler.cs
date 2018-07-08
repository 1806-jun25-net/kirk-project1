using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    [Serializable()]
    public class DataHandler
    {
        public List<Location> Locations { get; set; } = new List<Location>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<User> Users { get; set; } = new List<User>();

        public IngredientDirectory ingDir = new IngredientDirectory();
        public SizingPricingManager SPM { get; set; } = new SizingPricingManager();

        public DataHandler()
        {

        }

    }
}
