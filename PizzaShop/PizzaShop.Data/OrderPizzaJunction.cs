using System;
using System.Collections.Generic;

namespace PizzaShop.Data
{
    public partial class OrderPizzaJunction
    {
        public int PizzaId { get; set; }
        public int? Quantity { get; set; }
        public int OrderId { get; set; }

        public Orders Order { get; set; }
        public Pizzas Pizza { get; set; }
    }
}
