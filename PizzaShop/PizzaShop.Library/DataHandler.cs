﻿using PizzaShop.Data;
using PizzaShop.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

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

        [XmlIgnore]
        public LocationRepository LocRepo { get; set; }
        [XmlIgnore]
        public OrderRepository OrderRepo { get; set; }
        [XmlIgnore]
        public UserRepository UserRepo { get; set; }
        [XmlIgnore]
        public SizingPricingRepository SPRepo { get; set; }
        [XmlIgnore]
        public IngredientRepository IngRepo { get; set; }
        [XmlIgnore]
        public Project1DBContext db;

        //if it's passed a DBContext, will read from db
        public DataHandler(Project1DBContext dbParam)
        {
            db = dbParam;
            LocRepo = new LocationRepository(db);
            OrderRepo = new OrderRepository(db);
            UserRepo = new UserRepository(db);
            SPRepo = new SizingPricingRepository(db);
            IngRepo = new IngredientRepository(db);

            Locations = Mapper.Map(LocRepo.GetLocations()).ToList();
            Orders = Mapper.Map(OrderRepo.GetOrders()).ToList();
            Users = Mapper.Map(UserRepo.GetUsers()).ToList();

            SPM = Mapper.Map(SPRepo.GetSizingPricing().OrderBy( i => i.IngredientUsageScalar));
            foreach (var ing in Mapper.Map(IngRepo.GetIngredient()).ToList())
                ingDir.AddIngredient(ing);
        }

        // otherwise, will use default values
        public DataHandler()
        {

        }

    }
}
