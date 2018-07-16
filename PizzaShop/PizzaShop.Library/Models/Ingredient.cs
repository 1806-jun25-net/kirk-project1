using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PizzaShop.Library
{
    [Serializable]
    public class Ingredient
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }

        public Ingredient(string n, int q, string t) { Name = n; Quantity = q; Type = t; }

        public Ingredient() { }
    }
}
