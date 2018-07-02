using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    class OrderBuilder
    {
        public IOrder order;
        public IPizza ActivePizza { get; set; }

        public OrderBuilder(string user, string store, string size)
        {
            order = new Order(user, store);
            ActivePizza = new BuildYourOwnPizza(size);
        }
        public OrderBuilder(Order o)
        {
            order = new Order(o.UserID, o.Store, o.Pizzas);
            ActivePizza = o.Pizzas[0];
        }

        public void StartNewPizza()
        {

        }

        public void DuplicatePizza(int i)
        {

        }

        public void SwitchToPizza(int i)
        {

        }

        public void AddToppingToCurrentPizza(string topping)
        {
            
        }

        public void ChangeSauceOnCurrentPizza(string sauce)
        {

        }

        public void ChangeCrustOnCurrentPizza(string crust)
        {
            
        }

        public bool ChangeLocation(string store)
        {
            return false;
        }

        public List<IPizza> GetPizzas()
        {
            return null;
        }






        public string FinalizeOrder()
        {
            return null;
        }

        public bool IsOrderSmallEnough()
        {
            return false;
        }

        public bool IsOrderCheapEnough()
        {
            return false;
        }

        public bool IsOrderNotEmpty()
        {
            return false;
        }

        public bool IsOrderTwoHoursLater()
        {
            return false;
        }
    }
}
