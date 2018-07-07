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

        public DataHandler()
        {
            Locations.Add(new Location("Placeholder"));
            Locations.Add(new Location("Alternate Placeholder"));
            Users.Add("test", new User("test", "a", "b", "a@a.com", "1234567980", "Placeholder"));
            ingDir.AddIngredient(new Crust("classic crust", 1));
            ingDir.AddIngredient(new Sauce("classic sauce", 1));
            ingDir.AddIngredient(new Topping("cheese", 1));
            ingDir.AddIngredient(new Topping("pepperoni", 1));

        }
    }
}
