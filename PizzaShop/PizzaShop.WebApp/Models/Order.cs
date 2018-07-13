using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.WebApp.Models
{
    public class Order
    {
        public int Id { get; set; }  //Null on incomplete orders, Timestamp as ticks on completed order
        public DateTime Timestamp { get; set; }
        public string Store { get; set; }
        public string UserID { get; set; }
        public decimal Price { get; set; } //simplified from original order Property  Look here if Price stops calculating correctly*****
        public List<Pizza> Pizzas { get; set; }
    }
}
