using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.WebApp.Models
{
    public class Mapper
    {
        // single object conversions
        public static Library.Location Map(Location otherLoc) => new Library.Location
        {
            Name = otherLoc.Name,
            Stock = Map(otherLoc.Stock).ToList()
        };

        public static Location Map(Library.Location otherLoc) => new Location
        {
            Name = otherLoc.Name,
            Stock = Map(otherLoc.Stock).ToList()
        };


        public static Library.User Map(User otherUser) => new Library.User
        {
            Username = otherUser.Username,
            FirstName = otherUser.FirstName,
            LastName = otherUser.LastName,
            Email = otherUser.Email,
            Phone = otherUser.Phone,
            FavStore = otherUser.FavStore
        };

        public static User Map(Library.User otherUser) => new User
        {
            Username = otherUser.Username,
            FirstName = otherUser.FirstName,
            LastName = otherUser.LastName,
            Email = otherUser.Email,
            Phone = otherUser.Phone,
            FavStore = otherUser.FavStore
        };

        public static Library.Order Map(Order otherOrder) => new Library.Order
        {
            Id = otherOrder.Id,
            Timestamp = otherOrder.Timestamp,
            Store = otherOrder.Store,
            UserID = otherOrder.UserID,
            Price = otherOrder.Price,
            Pizzas = Map(otherOrder.Pizzas).ToList()
        };

        public static Order Map(Library.Order otherOrder) => new Order
        {
            Id = otherOrder.Id,
            Timestamp = otherOrder.Timestamp,
            Store = otherOrder.Store,
            UserID = otherOrder.UserID,
            Price = otherOrder.Price,
            Pizzas = Map(otherOrder.Pizzas).ToList()
        };

        public static Pizza Map(Library.Pizza otherPizza) => new Pizza
        {
            Size = otherPizza.Size,
            CrustType = otherPizza.CrustType,
            SauceType = otherPizza.SauceType,
            Toppings = otherPizza.Toppings,
            Price = otherPizza.Price
        };

        public static Library.Pizza Map(Pizza otherPizza) => new Library.Pizza
        {
            Size = otherPizza.Size,
            CrustType = otherPizza.CrustType,
            SauceType = otherPizza.SauceType,
            Toppings = otherPizza.Toppings,
            Price = otherPizza.Price
        };


        public static Ingredient Map(Library.Ingredient otherIngredient) => new Ingredient
        {
            Name = otherIngredient.Name,
            Quantity = otherIngredient.Quantity,
            Type = otherIngredient.Type
        };

        public static Library.Ingredient Map(Ingredient otherIngredient) => new Library.Ingredient
        {
            Name = otherIngredient.Name,
            Quantity = otherIngredient.Quantity,
            Type = otherIngredient.Type
        };



        public static IEnumerable<User> Map(IEnumerable<Library.User> otherUsers) => otherUsers.Select(Map);

        public static IEnumerable<Library.User> Map(IEnumerable<User> otherUsers) => otherUsers.Select(Map);

        public static IEnumerable<Order> Map(IEnumerable<Library.Order> otherOrders) => otherOrders.Select(Map);

        public static IEnumerable<Library.Order> Map(IEnumerable<Order> otherOrders) => otherOrders.Select(Map);

        public static IEnumerable<Location> Map(IEnumerable<Library.Location> otherLocations) => otherLocations.Select(Map);

        public static IEnumerable<Library.Location> Map(IEnumerable<Location> otherLocations) => otherLocations.Select(Map);

        public static IEnumerable<Pizza> Map(IEnumerable<Library.Pizza> otherPizzas) => otherPizzas.Select(Map);

        public static IEnumerable<Library.Pizza> Map(IEnumerable<Pizza> otherPizzas) => otherPizzas.Select(Map);

        public static IEnumerable<Ingredient> Map(IEnumerable<Library.Ingredient> otherIngredients) => otherIngredients.Select(Map);

        public static IEnumerable<Library.Ingredient> Map(IEnumerable<Ingredient> otherIngredients) => otherIngredients.Select(Map);

    }
}
