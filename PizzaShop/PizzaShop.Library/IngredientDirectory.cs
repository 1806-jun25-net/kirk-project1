﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class IngredientDirectory
    {
        public HashSet<string> Crusts { get; set; } = new HashSet<string>();
        public HashSet<string> Sauces { get; set; } = new HashSet<string>();
        public HashSet<string> Toppings { get; set; } = new HashSet<string>();

        public IngredientDirectory()
        {
            Crusts.Add("classic crust");
            Crusts.Add("thin crust");
            Sauces.Add("classic sauce");
            Sauces.Add("garlic white sauce");
            Toppings.Add("cheese");
            Toppings.Add("pepperoni");
            Toppings.Add("sausage");
        }

        public void AddIngredient(IIngredient ing)
        {
            if (ing is Crust)
                Crusts.Add(ing.Name);
            else if (ing is Sauce)
                Sauces.Add(ing.Name);
            else if (ing is Topping)
                Toppings.Add(ing.Name);
        }
    }
}
