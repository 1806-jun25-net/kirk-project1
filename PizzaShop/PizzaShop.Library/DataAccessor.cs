using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public static class DataAccessor
    {
        public static DataHandler DH { get; set; } = new DataHandler();
    }
}
