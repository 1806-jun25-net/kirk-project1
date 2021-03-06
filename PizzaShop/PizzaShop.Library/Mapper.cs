﻿using PizzaShop.Data;
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
            Stock = Map(otherLoc.LocationIngredientJunction),
            OrderHistory = Map(otherLoc.Orders).Select( o => o.Id ).ToList()
        };

        public static Data.Locations Map(Location otherLoc) => new Data.Locations
        {
            Name = otherLoc.Name
        };

        public static User Map(Data.Users otherUser) => new User
        {
            Username = otherUser.Username,
            FirstName = otherUser.FirstName,
            LastName = otherUser.LastName,
            Email = otherUser.Email,
            Phone = otherUser.Phone,
            FavStore = otherUser.FavLocation,
            OrderHistory = Map(otherUser.Orders).Select(m=> m.Id).ToList()
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
            UserID = otherOrder.UserId,
            Store = otherOrder.LocationId,
            Price = (decimal)otherOrder.Price,
            Pizzas = otherOrder.OrderPizzaJunction.Select( j => Mapper.Map(j.Pizza)).ToList()
        };

        public static Data.Orders Map(Order otherOrder) => new Data.Orders
        {
            Id = otherOrder.Id,
            Timestamp = otherOrder.Timestamp,
            UserId = otherOrder.UserID,
            LocationId = otherOrder.Store,
            Price = otherOrder.Price
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
            Type = otherIng.Type
        };

        public static Pizza Map(Data.Pizzas otherPizza) => new Pizza
        {
            Size = otherPizza.SizeId,
            Price = (decimal)otherPizza.Price,
            
            CrustType = otherPizza.PizzaIngredientJunction.First( i => i.Ingredient.Type.Equals("crust")).Ingredient.Name,
            SauceType = otherPizza.PizzaIngredientJunction.First(i => i.Ingredient.Type.Equals("sauce")).Ingredient.Name,
            Toppings = otherPizza.PizzaIngredientJunction.Where(i => i.Ingredient.Type.Equals("topping")).Select(j => j.Ingredient.Name).ToHashSet()
        };

        public static Data.Pizzas Map(Pizza otherPizza) => new Data.Pizzas
        {
            SizeId = otherPizza.Size,
            Price = otherPizza.Price,

            //PizzaIngredientJunction
            
        };

        public static List<Ingredient> Map(ICollection<LocationIngredientJunction> lij)
        {
            List<Ingredient> stock = new List<Ingredient>();
            if (lij == null)
            {
                return stock;
            }
            foreach (var i in lij)
            {
                stock.Add(new Ingredient(i.IngredientId, (int)i.Quantity, i.Ingredient.Type));
            }
            return stock;
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
