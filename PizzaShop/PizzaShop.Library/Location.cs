using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class Location
    {
        public Dictionary<string, IIngredient> Stock { get; set; }

        /// <summary>
        /// Increases this Location's quantity of given IIngredient by amount specified in parameter's quantity
        /// If IIngredient is not currently stocked at this location, adds it to stock at given quantity
        /// </summary>
        /// <param name="ing"></param>
        void AddStock(IIngredient ing)
        {

        }

        /// <summary>
        /// Decreases this Location's quantity of given IIngredient by amount specified in parameter's quantity
        /// If this decrease would put quantity into the negative, leaves quantity unchanged.
        /// </summary>
        /// <param name="ing"></param>
        /// <returns>null if successful, Name of IIngredient if removal would make quantity negative</returns>
        string RemoveStock(IIngredient ing)
        {

            return "";
        }

        /// <summary>
        /// Increases this Location's quantity of all given IIngredients by amount specified in parameter's quantity for each
        /// If any IIngredients are not currently stocked at this location, adds it to stock at given quantity
        /// </summary>
        /// <param name="list"></param>
        void AddBulkStock(List<IIngredient> list)
        {
            
        }

        /// <summary>
        /// Edcreases this Location's quantity of all given IIngredients by amount specified in parameter's quantity for each
        /// If any such decrease would put any quantity into the negative, leaves all quantities unchanged.
        /// </summary>
        /// <param name="list"></param>
        /// <returns>null if successful, Name of first offending IIngredient which would make quantity negative otherwise</returns>
        string RemoveBulkStock(List<IIngredient> list)
        {
            return "";
        }
    }
}
