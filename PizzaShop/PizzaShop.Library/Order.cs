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
        public string UserID { get;}
        public string Id { get; set;  }  //for internal use only.  Null on incomplete orders, Timestamp as ticks on completed order until sql gets added
        public decimal Price { get; set; }

        //fresh order from scratch
        public Order(string userParam, string storeParam)
        {
            Pizzas = new List<IPizza>();
            //get unique order number somehow
            Id = null;
            UserID = userParam;
            Store = storeParam;
        }

        // importing suggested order for user
        public Order(string userParam, string storeParam, List<IPizza> pizzaParam)
        {
            Pizzas = pizzaParam;
            //get unique order number somehow
            Id = null;
            UserID = userParam;
            Store = storeParam;
        }


    }
}
 