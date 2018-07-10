using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Library
{
    public static class Mapper
    {

        // single object conversions
        public static Location Map(Data.Locations otherLoc) => new Location
        {
            Name = otherLoc.Name,
            //Reviews = Map(otherLoc.Review).ToList()
            //Stock = Map(otherLoc.LocationIngredientJunction),
            //OrderHistory = Map(otherLoc.Orders)
        };

        public static Data.Locations Map(Location otherLoc) => new Data.Locations
        {
            Name = otherLoc.Name,
            //Review = Map(restaurant.Reviews).ToList()
            //LocationIngredientJunction = Map(otherLoc.Stock),
            //Orders = Map(otherLoc.OrderHistory)
        };

        public static User Map(Data.Users otherUser) => new User
        {
            Username = otherUser.Username,
            FirstName = otherUser.FirstName,
            LastName = otherUser.LastName,
            Email = otherUser.Email,
            Phone = otherUser.Phone,
            FavStore = otherUser.FavLocation,
            //OrderHistory = Map(otherUser.Orders)
        };

        public static Data.Users Map(User otherUser) => new Data.Users
        {
            Username = otherUser.Username,
            FirstName = otherUser.FirstName,
            LastName = otherUser.LastName,
            Email = otherUser.Email,
            Phone = otherUser.Phone,
            FavLocation = otherUser.FavStore,
            //Orders = Map(otherUser.OrderHistory)
        };

        public static Order Map(Data.Orders otherOrder) => new Order
        {
            Id = otherOrder.Id,
            Timestamp = (DateTime)otherOrder.Timestamp,
            UserID = otherOrder.User.Username,
            Store = otherOrder.Location.Name,
            Price = (decimal)otherOrder.Price,
            //Pizzas = Map(otherOrder.OrderPizzaJunction)
        };

        public static Data.Orders Map(Order otherOrder) => new Data.Orders
        {
            Id = otherOrder.Id,
            Timestamp = (DateTime)otherOrder.Timestamp,
            User = Map(DataAccessor.GetUserByUsername(otherOrder.UserID)),
            Location = Map(DataAccessor.GetLocationByName(otherOrder.Store)),
            Price = otherOrder.Price,
            //Pizzas = Map(otherOrder.OrderPizzaJunction)
        };

        public static Ingredient Map(Data.Ingredients otherIng) => new Ingredient
        {
            Name = otherIng.Name,
            Type = otherIng.Type,
            //Quantity = Map(otherIng.LocationIngredientJunction) 
        };

        public static Data.Ingredients Map(Ingredient otherIng) => new Data.Ingredients
        {
            Name = otherIng.Name,
            Type = otherIng.Type,
            //??something added to the locationingredientjunction??
        };

        public static Pizza Map(Data.Pizzas otherPizza) => new Pizza
        {
            Size = otherPizza.Size.Size,
            Price = (decimal)otherPizza.Price,
            //Eventually add other toppings, for now keep things standard
            CrustType = "classic crust",
            SauceType = "classic sauce",
            Toppings = { "cheese" }
        };

        public static Data.Pizzas Map(Pizza otherPizza) => new Data.Pizzas
        {
            Size = new Data.SizingPricing(otherPizza.Size, DataAccessor.DH.SPM.GetBasePrice(otherPizza.Size), DataAccessor.DH.SPM.GetToppingPrice(otherPizza.Size), DataAccessor.DH.SPM.GetIngredientUsageScalar(otherPizza.Size)),
            Price = otherPizza.Price
        };

        
        public static SizingPricingManager Map(Data.SizingPricing otherPricing)
        {
            SizingPricingManager spm = new SizingPricingManager();
            spm.AddNewSize(otherPricing.Size, (decimal)otherPricing.BasePrice, (decimal)otherPricing.ToppingPrice, (int)otherPricing.IngredientUsageScalar);
            return spm;
        }

        public static SizingPricingManager Map(IEnumerable<Data.SizingPricing> otherPricing)
        {
            SizingPricingManager spm = new SizingPricingManager();
            foreach (var op in otherPricing)
                spm.AddNewSize(op.Size, (decimal)op.BasePrice, (decimal)op.ToppingPrice, (int)op.IngredientUsageScalar);
            return spm;
        }


        public static IEnumerable<User> Map(IEnumerable<Data.Users> otherUsers) => otherUsers.Select(Map);

        public static IEnumerable<Data.Users> Map(IEnumerable<User> otherUsers) => otherUsers.Select(Map);

        public static IEnumerable<Order> Map(IEnumerable<Data.Orders> otherOrders) => otherOrders.Select(Map);

        public static IEnumerable<Data.Orders> Map(IEnumerable<Order> otherOrders) => otherOrders.Select(Map);

        public static IEnumerable<Location> Map(IEnumerable<Data.Locations> otherLocations) => otherLocations.Select(Map);

        public static IEnumerable<Data.Locations> Map(IEnumerable<Location> otherLocations) => otherLocations.Select(Map);

        public static IEnumerable<Pizza> Map(IEnumerable<Data.Pizzas> otherPizzas) => otherPizzas.Select(Map);

        public static IEnumerable<Data.Pizzas> Map(IEnumerable<Pizza> otherPizzas) => otherPizzas.Select(Map);

        public static IEnumerable<Ingredient> Map(IEnumerable<Data.Ingredients> otherIngredients) => otherIngredients.Select(Map);

        public static IEnumerable<Data.Ingredients> Map(IEnumerable<Ingredient> otherIngredients) => otherIngredients.Select(Map);





    }
}
