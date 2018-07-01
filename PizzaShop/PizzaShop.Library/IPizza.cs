using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public interface IPizza
    {
        string Size { get; }
        string CrustType { get; }
        string SauceType { get; }
        HashSet<String> Toppings { get; set; }
        decimal Price { get; }
    }
}