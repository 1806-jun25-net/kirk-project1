//Functionality Still Required:
//Order history
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Library
{
    public class Location
    {
        public List<Ingredient> Stock { get; set; } = new List<Ingredient>();
        public List<int> OrderHistory { get; set; } = new List<int>();
        public String Name { get; set; }

        public Location(String name)
        {
            Name = name;
            Stock = new List<Ingredient>();
            OrderHistory = new List<int>();
        }

        public Location()
        {
            Stock = new List<Ingredient>();
            OrderHistory = new List<int>();
            Name = "";
        }

        /// <summary>
        /// Increases this Location's quantity of given IIngredient by amount specified in parameter's quantity
        /// If IIngredient is not currently stocked at this location, adds it to stock at given quantity
        /// </summary>
        /// <param name="ing"></param>
        public void AddStock(Ingredient ing)
        {
            if (Stock.Any(t => t.Name.Equals(ing.Name)))
                Stock.First(t => t.Name.Equals(ing.Name)).Quantity += ing.Quantity;
            else
            {
                Stock.Add(ing);
                //also register ingredient in ingredient directory
                DataAccessor.DH.ingDir.AddIngredient(ing);
            }
        }

        /// <summary>
        /// Decreases this Location's quantity of given IIngredient by amount specified in parameter's quantity
        /// If this decrease would put quantity into the negative, leaves quantity unchanged.
        /// If this decrease puts quantity to 0, removes that IIngredient from the stock dictionary
        /// </summary>
        /// <param name="ing"></param>
        /// <returns>null if successful, Name of IIngredient if removal would make quantity negative</returns>
        public string RemoveStock(Ingredient ing)
        {
            //1st - is the ingredient in our dictionary?
            if (!Stock.Any(t => t.Name.Equals(ing.Name)))
                return ing.Name;

            //2nd - does ingredient have sufficient quantity?
            if (Stock.First(t => t.Name.Equals(ing.Name)).Quantity < ing.Quantity)
                return ing.Name;

            //3rd - decrease quantity
            Stock.First(t => t.Name.Equals(ing.Name)).Quantity -= ing.Quantity;

            /*Disabling ingredient removal per Nick since it may cause conflict with DB

            //4th - check if quantity is now 0
            if (Stock.First(t => t.Name.Equals(ing.Name)).Quantity == 0)
                Stock.Remove(Stock.First(t => t.Name.Equals(ing.Name)));

            */

            return null;
        }

        /// <summary>
        /// Increases this Location's quantity of all given IIngredients by amount specified in parameter's quantity for each
        /// If any IIngredients are not currently stocked at this location, adds it to stock at given quantity
        /// </summary>
        /// <param name="list"></param>
        public void AddBulkStock(List<Ingredient> list)
        {
            foreach (var item in list)
            {
                AddStock(item);
            }
        }

        /// <summary>
        /// Decreases this Location's quantity of all given IIngredients by amount specified in parameter's quantity for each
        /// If any such decrease would put any quantity into the negative, leaves all quantities unchanged.
        /// </summary>
        /// <param name="list"></param>
        /// <returns>null if successful, Name of first offending IIngredient which would make quantity negative otherwise</returns>
        public string RemoveBulkStock(List<Ingredient> list)
        {
            //1st - Are all ingredients in dictionary?
            //2nd - Do all ingredients have sufficient quantity?
            foreach (var item in list)
            {
                if(!Stock.Any(t => t.Name.Equals(item.Name)) || Stock.First(t => t.Name.Equals(item.Name)).Quantity < item.Quantity)
                    return item.Name;
            }

            //3rd - decrease quantity for all ingredients
            foreach (var item in list)
            {
                Stock.First(t => t.Name.Equals(item.Name)).Quantity -= item.Quantity;
            }
            //4th - check if quantity is now 0, remove if so
            foreach (var item in list)
            {
                if (Stock.First(t => t.Name.Equals(item.Name)).Quantity == 0)
                    Stock.Remove(Stock.First(t => t.Name.Equals(item.Name)));
            }
            return null;
        }
    }
}
