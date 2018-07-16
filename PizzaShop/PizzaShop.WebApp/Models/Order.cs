using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.WebApp.Models
{
    public class Order
    {
        [Display(Name = "Order ID")]
        public int Id { get; set; }  //Null on incomplete orders, Timestamp as ticks on completed order
        public DateTime Timestamp { get; set; }
        [Display(Name = "Order Location")]
        public string Store { get; set; }
        [Display(Name = "Username")]
        public string UserID { get; set; }
        [Display(Name = "Order Total")]
        public decimal Price { get; set; } //simplified from original order Property  Look here if Price stops calculating correctly*****
        public List<Pizza> Pizzas { get; set; }
    }
}
