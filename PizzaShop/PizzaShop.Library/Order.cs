using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{


    public class Order : IOrder
    {
        public List<IPizza> Pizzas { get; set; }
        public DateTime Timestamp { get; set; }
        public string Store { get; set; }
        public string UserID { get; set; }
        public int Id { get; }

        public Order(string userParam, string storeParam)
        {
            Pizzas = new List<IPizza>();
            //get unique order number somehow
            Id = 0;
            UserID = userParam;
            Store = storeParam;
        }

        public Order(string userParam, string storeParam, List<IPizza> pizzaParam)
        {
            Pizzas = pizzaParam;
            //get unique order number somehow
            Id = 0;
            UserID = userParam;
            Store = storeParam;
        }

    }
}
 