using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class IngredientDirectory
    {
        public HashSet<string> Crusts { get; set; } = new HashSet<string>();
        public HashSet<string> Sauces { get; set; } = new HashSet<string>();
        public HashSet<string> Toppings { get; set; } = new HashSet<string>();


        public void AddIngredient(Ingredient ing)
        {
            if (ing.Type.ToLower().Equals("crust"))
                Crusts.Add(ing.Name);
            else if (ing.Type.ToLower().Equals("sauce"))
                Sauces.Add(ing.Name);
            else if (ing.Type.ToLower().Equals("topping"))
                Toppings.Add(ing.Name);
        }
    }
}
