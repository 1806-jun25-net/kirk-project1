﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public interface IOrder
    {
        List<IPizza> Pizzas { get; set; }
        DateTime Timestamp { get; set; }
        String Store { get; set; }
        String UserID { get; set; }
        int Id { get; }
    }
}
