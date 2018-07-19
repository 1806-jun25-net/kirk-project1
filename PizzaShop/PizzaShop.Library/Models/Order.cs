using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Library
{

    [Serializable]
    public class Order
    {
        public int Id { get; set; }  //Null on incomplete orders, Timestamp as ticks on completed order
        public DateTime Timestamp { get; set; }
        public string Store { get; set; }
        public string UserID { get; set; }
        [JsonIgnore]
        private decimal _price;
        public decimal Price
        {
            get
            {
                return Pizzas.Sum(p => p.Price);
            }
            set
            {
                _price = value;
            }
        }
        public List<Pizza> Pizzas { get; set; }

        //fresh order from scratch
        public Order(string userParam, string storeParam)
        {
            Pizzas = new List<Pizza>();
            //get unique order number somehow
            UserID = userParam;
            Store = storeParam;
            Price = 0;
        }

        // importing suggested order for user
        public Order(string userParam, string storeParam, List<Pizza> pizzaParam)
        {
            Pizzas = pizzaParam;
            //get unique order number somehow
            UserID = userParam;
            Store = storeParam;
            Price = pizzaParam.Sum(p => p.Price);
        }

        public Order() { }



    }
}
 