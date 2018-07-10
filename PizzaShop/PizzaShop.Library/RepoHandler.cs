using PizzaShop.Data;
using PizzaShop.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Variation Of DataHandler, but accesses repo/database rather than

namespace PizzaShop.Library
{
    public class RepoHandler
    {
        public LocationRepository LocRepo { get; set; }
        public OrderRepository OrderRepo { get; set; }
        public UserRepository UserRepo { get; set; }
        public SizingPricingRepository SPRepo { get; set; }
        public IngredientRepository IngRepo { get; set; }

        public List<Location> Locations { get; set; }
        public List<Order> Orders { get; set; }
        public List<User> Users { get; set; }

        public IngredientDirectory ingDir = new IngredientDirectory();
        public SizingPricingManager SPM { get; set; } = new SizingPricingManager();

        public RepoHandler(Project1DBContext db)
        {
            LocRepo = new LocationRepository(db);
            OrderRepo = new OrderRepository(db);
            UserRepo = new UserRepository(db);
            SPRepo = new SizingPricingRepository(db);
            IngRepo = new IngredientRepository(db);

            Locations = Mapper.Map(LocRepo.GetLocations()).ToList();
            Orders = Mapper.Map(OrderRepo.GetOrders()).ToList();
            Users = Mapper.Map(UserRepo.GetUsers()).ToList();

            SPM = Mapper.Map(SPRepo.GetSizingPricing());
            foreach (var ing in Mapper.Map(IngRepo.GetIngredient()).ToList())
                ingDir.AddIngredient(ing);
        }
    }
}
