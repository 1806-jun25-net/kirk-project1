using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public interface IPizza
    {
        string Size { get; set; }
        string CrustType { get; set; }
        string SauceType { get; set; }
        HashSet<String> Toppings { get; set; }
        decimal Price { get; set; }
    }
}