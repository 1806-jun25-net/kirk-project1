using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class OrderBuilder
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

        public void StartNewPizza(string size)
        {
            ActivePizza = new BuildYourOwnPizza(size);
        }

        public void DuplicatePizza(int i)
        {
            if (i >= 0 && i < order.Pizzas.Count)
                order.Pizzas.Add(order.Pizzas[i]);
        }

        public void AddPizza(IPizza p)
        {
            if (p != null)
                order.Pizzas.Add(p);
        }

        public void SwitchActivePizza(int i)
        {
            if (i >= 0 && i < order.Pizzas.Count)
                ActivePizza = order.Pizzas[i];
        }

        public void AddToppingToActivePizza(string topping)
        {
            //TODO: check topping is valid from list of toppings
            ActivePizza.Toppings.Add(topping);
            ActivePizza.Price += SizingPricingAccessor.SPM.GetToppingPrice(ActivePizza.Size);
        }

        public void RemoveToppingFromActivePizza(string topping)
        {
            //TODO: check topping is valid from list of toppings

            //TODO: verify topping is already on pizza before removal
            ActivePizza.Toppings.Remove(topping);
            ActivePizza.Price -= SizingPricingAccessor.SPM.GetToppingPrice(ActivePizza.Size);
        }

        public void ChangeSauceOnActivePizza(string sauce)
        {
            //TODO: check sauce is valid from list of toppings
            ActivePizza.SauceType = sauce;
        }

        public void ChangeCrustOnActivePizza(string crust)
        {
            //TODO: check crust is valid from list of 
            ActivePizza.CrustType = crust;
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
