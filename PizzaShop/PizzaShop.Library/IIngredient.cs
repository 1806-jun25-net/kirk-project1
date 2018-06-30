using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public interface IIngredient
    {
        string Name { get;}
        int Quantity { get; set; }
    }
}
