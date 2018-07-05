using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class OrderBuilder
    {
        public IOrder order;
        public IPizza ActivePizza { get; set; } = null;
        public const int maxPizzas = 12;
        public const decimal maxOrderPrice = 500m;

        public OrderBuilder(string user, string store)
        {
            order = new Order(user, store);
        }
        public OrderBuilder(string user, string store, List<IPizza> pizzas)
        {
            order = new Order(user, store, pizzas);
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

        public void AddActivePizza()
        {
            if (ActivePizza != null)
                AddPizza(ActivePizza);
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
            ActivePizza.Price += DataAccessor.DH.SPM.GetToppingPrice(ActivePizza.Size);
        }

        public void RemoveToppingFromActivePizza(string topping)
        {
            //TODO: check topping is valid from list of toppings

            //TODO: verify topping is already on pizza before removal
            if (ActivePizza.Toppings.Contains(topping))
            {
                ActivePizza.Toppings.Remove(topping);
                ActivePizza.Price -= DataAccessor.DH.SPM.GetToppingPrice(ActivePizza.Size);
            }
        }

        public void ChangeSauceOnActivePizza(string sauce)
        {
            //TODO: check sauce is valid from list of toppings
            if (sauce != null)
                ActivePizza.SauceType = sauce;
        }

        public void ChangeCrustOnActivePizza(string crust)
        {
            //TODO: check crust is valid from list of 
            if (crust != null)
                ActivePizza.CrustType = crust;
        }

        public bool ChangeLocation(string store)
        {
            //TODO: check location is valid
            if (store != null)
                order.Store = store;
            return false;
        }

        public List<IPizza> GetPizzas()
        {
            return order.Pizzas;
        }

        public void ChangeSizeOfActivePizza(string size)
        {
            throw new Exception("Yet to implement change size");
        }




        public string FinalizeOrder()
        {
            //check everything is valid
            //return with reason if not valid
            string result;
            if (!IsOrderSmallEnough())
                return $"Too many pizzas in this order.  Orders may only have {maxPizzas} pizzas maximum.";
            if (!IsOrderCheapEnough())
                return $"Too expenseive.  Maximum order price total is ${maxOrderPrice}.";
            if (!IsOrderNotEmpty())
                return "Order must have at least one pizza.";
            if (!IsOrderTwoHoursLater())
                return "Order is being placed too soon after a recent order.  You may place one order with each location every two hours.";
            if ((result = DoesLocationHaveAllIngredients()) != null)
                return $"Chosen location does not have the necessairy ingredients for all your pizzas.  It is short on {result}";

            //if valid generate timestamp& order ID
            order.Timestamp = DateTime.Now;
            order.Id = order.Timestamp.Ticks.ToString();

            //TODO:  add order to order history
            return null;
        }

        public bool IsOrderSmallEnough()
        {
            if (order.Pizzas.Capacity > maxPizzas)
                return false;
            return true;
        }

        public bool IsOrderCheapEnough()
        {
            if (order.Price > maxOrderPrice)
                return false;
            return true;
        }

        public bool IsOrderNotEmpty()
        {
            if (order.Pizzas.Capacity <= 0)
                return false;
            return true;
        }

        public bool IsOrderTwoHoursLater()
        {
            //TODO: actually figure out how to do this

            return false;
        }

        public string DoesLocationHaveAllIngredients()
        {
            // User Remove Stock Bulk from store
            //maybe add getLocation method??
            return null;
        }
    }
}
