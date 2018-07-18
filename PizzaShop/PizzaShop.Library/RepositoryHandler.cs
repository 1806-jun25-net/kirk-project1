using PizzaShop.Data;
using PizzaShop.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PizzaShop.Library
{
    [Serializable()]
    public class RepositoryHandler
    {

        public LocationRepository LocRepo { get; set; }
        public OrderRepository OrderRepo { get; set; }
        public UserRepository UserRepo { get; set; }
        public SizingPricingRepository SPRepo { get; set; }
        public IngredientRepository IngRepo { get; set; }
        private readonly Project1DBContext db;
        public PizzaRepository PizzaRepo { get; set; }

        //if it's passed a DBContext, will read from db
        public RepositoryHandler(Project1DBContext dbParam)
        {
            db = dbParam;
            LocRepo = new LocationRepository(db);
            OrderRepo = new OrderRepository(db);
            UserRepo = new UserRepository(db);
            SPRepo = new SizingPricingRepository(db);
            IngRepo = new IngredientRepository(db);
            PizzaRepo = new PizzaRepository(db);
        }

    }
}
