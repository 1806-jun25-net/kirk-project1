using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Library
{
    public class OrderBuilder
    {
        public Order order;
        public Pizza ActivePizza { get; set; } = null;
        public const int maxPizzas = 12;
        public const decimal maxOrderPrice = 500m;

        public OrderBuilder(string user, string store)
        {
            order = new Order(user, store);
            ActivePizza = null;
        }
        public OrderBuilder(string user, string store, List<Pizza> pizzas)
        {
            order = new Order(user, store, pizzas);
            ActivePizza = null;
        }

        public void StartNewPizza(string size)
        {
            ActivePizza = new Pizza(size);
        }

        public void DuplicatePizza(int i)
        {
            if (i >= 0 && i < order.Pizzas.Count)
            {
                order.Price += order.Pizzas[i].Price;
                order.Pizzas.Add(order.Pizzas[i]);
            }
        }

        public void AddPizza(Pizza p)
        {
            if (p != null)
            {
                order.Pizzas.Add(p);
                order.Price += p.Price;
            }
        }

        public void AddActivePizza()
        {
            if (ActivePizza != null)
            {
                AddPizza(ActivePizza);
                order.Price += ActivePizza.Price;
            }
        }

        public void SwitchActivePizza(int i)
        {
            if (i >= 0 && i < order.Pizzas.Count && order.Pizzas[i] != null)
                ActivePizza = order.Pizzas[i];
        }

        public bool AddToppingToActivePizza(string topping)
        {
            // if topping is not in valid list of toppings
            if (!DataAccessor.DH.ingDir.Toppings.Contains(topping))
                return false;
            //if topping already on pizza
            if (ActivePizza.Toppings.Contains(topping))
                return false;
            ActivePizza.Toppings.Add(topping);
            ActivePizza.Price += DataAccessor.DH.SPM.GetToppingPrice(ActivePizza.Size);
            return true;
        }

        public bool RemoveToppingFromActivePizza(string topping)
        {

            if (ActivePizza.Toppings.Contains(topping))
            {
                ActivePizza.Toppings.Remove(topping);
                ActivePizza.Price -= DataAccessor.DH.SPM.GetToppingPrice(ActivePizza.Size);
                return true;
            }
            return false;
        }

        public bool ChangeSauceOnActivePizza(string sauce)
        {
            //if sauce is not from valid list of toppings
            if (!DataAccessor.DH.ingDir.Sauces.Contains(sauce))
                return false;
            ActivePizza.SauceType = sauce;
            return true;
        }

        public bool ChangeCrustOnActivePizza(string crust)
        {
            // if topping is not in valid list of toppings
            if (!DataAccessor.DH.ingDir.Crusts.Contains(crust))
                return false;
            ActivePizza.CrustType = crust;
            return true;
        }

        public bool ChangeSizeOfActivePizza(string size)
        {
            // if size is not in valid list of sizes
            if (!DataAccessor.DH.SPM.Sizes.Contains(size))
                return false;
            ActivePizza.Size = size;
            ActivePizza.Price = DataAccessor.DH.SPM.GetBasePrice(size) + DataAccessor.DH.SPM.GetToppingPrice(size) * ActivePizza.Toppings.Count;
            return true;
        }

        public bool ChangeLocation(string store)
        {
            //TODO: check location is valid
            if (store != null)
                order.Store = store;
            return false;
        }

        public List<Pizza> GetPizzas()
        {
            return order.Pizzas;
        }

        public decimal CalculateTotalPrice()
        {
            decimal total = order.Pizzas.Sum(p => p.Price);
            order.Price = total;
            return total;
        }

        public Boolean RemovePizza(int i)
        {
            if (i >= 0 && i < order.Pizzas.Count)
            {
                order.Price -= order.Pizzas[i].Price;
                order.Pizzas.Remove(order.Pizzas[i]);
                return true;
            }
            return false;
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

            //add order to order history
            DataAccessor.DH.Orders.Add(order);
            //add orderID to user order history
            DataAccessor.GetUserByUsername(order.UserID).OrderHistory.Add(order.Id);
            //add orderID to location order history
            DataAccessor.DH.Locations.First(l => l.Name.Equals(order.Store)).OrderHistory.Add(order.Id);
            return null;
        }

        public bool IsOrderSmallEnough()
        {
            if (order.Pizzas.Count > maxPizzas)
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
            if (order.Pizzas.Count <= 0)
                return false;
            return true;
        }

        public bool IsOrderTwoHoursLater()
        {
            //Both Users and Locations have an order history contianing order ids
            //Find intersection of User & location order histories from newest to oldest
            //if newest shared order <2 hrs reject, otherwise accept
            List<String> userOrders = DataAccessor.GetUserByUsername(order.UserID).OrderHistory;
            List<String> locationOrders = DataAccessor.DH.Locations.First(l => l.Name.Equals(order.Store)).OrderHistory;
            DateTime orderTime;
            //new orders are always added to the end of the OrderHistory list, so go through newest orders first
            for (int i = userOrders.Count-1; i >= 0; i--)
            {
                //get the order time off the user order we want to check
                orderTime = DataAccessor.GetOrderByID(userOrders[i]).Timestamp;
                //if the most recent order being checked is already older than two hours
                //this is just for efficiency to not have to go through a user's entire order history since the beginning of time
                if (DateTime.Compare(DateTime.Now, orderTime.AddHours(2)) > 0)
                {
                    return true;
                }

                 //if an orderID is shared between user and location
                if (locationOrders.Contains(userOrders[i]))
                {
                    orderTime = DataAccessor.GetOrderByID(userOrders[i]).Timestamp;
                    //if order was placed within the last two hours
                    if ( DateTime.Compare(DateTime.Now, orderTime.AddHours(2)) < 0 )
                    {
                        return false;
                    }

                }
            }
            
            return true;
        }

        public string DoesLocationHaveAllIngredients()
        {
            //1: generate -List- of all ingredient types w/ appropiate quantity based on scalar
            List<Ingredient> allIngredients = BuildIngredientList();
            Location loc = DataAccessor.DH.Locations.First( l => l.Name.Equals(order.Store));
            return loc.RemoveBulkStock(allIngredients);
        }

        public List<Ingredient> BuildIngredientList()
        {
            List<Ingredient> allIngredients = new List<Ingredient>();
            int amount;
            foreach (var p in order.Pizzas)
            {
                amount = DataAccessor.DH.SPM.GetIngredientUsageScalar(p.Size);
                AddToIngredientList(allIngredients, new Ingredient(p.CrustType, amount, "crust"));
                AddToIngredientList(allIngredients, new Ingredient(p.SauceType, amount, "sauce"));
                foreach (var s in p.Toppings)
                {
                    AddToIngredientList(allIngredients, new Ingredient(s, amount, "topping"));
                }
            }

            return allIngredients;
        }

        public void AddToIngredientList(List<Ingredient> allIngredients, Ingredient ing)
        {
            int index = allIngredients.IndexOf(ing);
            if (index == -1)  //ingredient not yet in list
            {
                allIngredients.Add(ing);
            }
            else   // increase quantity of ingredient already in list
            {
                allIngredients[index].Quantity += ing.Quantity;
            }
        }

    }
}
