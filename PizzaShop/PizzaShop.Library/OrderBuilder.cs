using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Library
{
    [Serializable]
    public class OrderBuilder
    {
        public Order CurOrder { get; set; }
        public Pizza ActivePizza { get; set; }
        private readonly int MaxPizzas = 12;
        private readonly decimal MaxOrderPrice = 500m;

        public OrderBuilder(string user, string store)
        {
            CurOrder = new Order(user, store);
            ActivePizza = null;
        }
        public OrderBuilder(string user, string store, List<Pizza> pizzas)
        {
            CurOrder = new Order(user, store, pizzas);
            ActivePizza = null;
        }

        public OrderBuilder(Order o)
        {
            CurOrder = o;
            ActivePizza = null;
        }

        public OrderBuilder() { }

        public bool StartNewPizza(string size, RepositoryHandler DH)
        {
            if (!DH.SPRepo.ContainsSize(size))
                return false;
            ActivePizza = new Pizza(size);
            ActivePizza.Price = DH.SPRepo.GetBasePrice(size) + DH.SPRepo.GetToppingPrice(size);
            return true;
        }

        public void DuplicatePizza(int i)
        {
            if (i >= 0 && i < CurOrder.Pizzas.Count)
            {
                CurOrder.Price += CurOrder.Pizzas[i].Price;
                CurOrder.Pizzas.Add(CurOrder.Pizzas[i]);
            }
        }

        public void AddPizza(Pizza p)
        {
            if (p != null)
            {
                CurOrder.Pizzas.Add(p);
                CurOrder.Price += p.Price;
            }
        }

        public void AddActivePizza()
        {
            if (ActivePizza != null)
            {
                AddPizza(ActivePizza);
            }
        }

        public void SwitchActivePizza(int i)
        {
            if (i >= 0 && i < CurOrder.Pizzas.Count && CurOrder.Pizzas[i] != null)
                ActivePizza = CurOrder.Pizzas[i];
        }

        public bool AddToppingToActivePizza(string topping, RepositoryHandler DH)
        {
            // if topping is not in valid list of toppings
            if ( !( DH.IngRepo.GetIngredients().Any( t => t.Name.Equals(topping) && t.Type.Equals("topping"))) )
                return false;
            //if topping already on pizza
            if (ActivePizza.Toppings.Contains(topping))
                return false;
            ActivePizza.Toppings.Add(topping);
            ActivePizza.Price += DH.SPRepo.GetToppingPrice(ActivePizza.Size);
            return true;
        }

        public bool RemoveToppingFromActivePizza(string topping, RepositoryHandler DH)
        {

            if (ActivePizza.Toppings.Contains(topping))
            {
                ActivePizza.Toppings.Remove(topping);
                ActivePizza.Price -= DH.SPRepo.GetToppingPrice(ActivePizza.Size);
                return true;
            }
            return false;
        }

        public bool ChangeSauceOnActivePizza(string sauce, RepositoryHandler DH)
        {
            //if sauce is not from valid list of toppings
            if (!(DH.IngRepo.GetIngredients().Any(t => t.Name.Equals(sauce) && t.Type.Equals("sauce"))))
                return false;
            ActivePizza.SauceType = sauce;
            return true;
        }

        public bool ChangeCrustOnActivePizza(string crust, RepositoryHandler DH)
        {
            // if topping is not in valid list of toppings
            if (!(DH.IngRepo.GetIngredients().Any(t => t.Name.Equals(crust) && t.Type.Equals("crust"))))
                return false;
            ActivePizza.CrustType = crust;
            return true;
        }

        public bool ChangeSizeOfActivePizza(string size, RepositoryHandler DH)
        {
            // if size is not in valid list of sizes
            if (!DH.SPRepo.ContainsSize(size))
                return false;
            ActivePizza.Size = size;
            ActivePizza.Price = DH.SPRepo.GetBasePrice(size) + DH.SPRepo.GetToppingPrice(size) * ActivePizza.Toppings.Count;
            return true;
        }

        public bool ChangeLocation(string store)
        {
            //TODO: check location is valid
            if (store != null)
                CurOrder.Store = store;
            return false;
        }

        public List<Pizza> GetPizzas()
        {
            return CurOrder.Pizzas;
        }

        public decimal CalculateTotalPrice()
        {
            decimal total = CurOrder.Pizzas.Sum(p => p.Price);
            CurOrder.Price = total;
            return total;
        }

        public Boolean RemovePizza(int i)
        {
            if (i >= 0 && i < CurOrder.Pizzas.Count)
            {
                CurOrder.Price -= CurOrder.Pizzas[i].Price;
                CurOrder.Pizzas.Remove(CurOrder.Pizzas[i]);
                return true;
            }
            return false;
        }


        public string FinalizeOrder(RepositoryHandler DH)
        {
            //check everything is valid
            //return with reason if not valid
            string result;
            if (!IsOrderSmallEnough())
                return $"Too many pizzas in this order.  Orders may only have {MaxPizzas} pizzas maximum.";
            if (!IsOrderCheapEnough())
                return $"Too expenseive.  Maximum order price total is ${MaxOrderPrice}.";
            if (!IsOrderNotEmpty())
                return "Order must have at least one pizza.";
            if (!IsOrderTwoHoursLaterV2(DH))
                return "Order is being placed too soon after a recent order.  You may place one order with each location every two hours.";
            if ((result = DoesLocationHaveAllIngredients(DH)) != null)  
                return $"Chosen location does not have the necessairy ingredients for all your pizzas.  It is short on {result}";

            //if valid generate timestamp& order ID
            CurOrder.Timestamp = DateTime.Now;
            CurOrder.Id = Math.Abs((int)CurOrder.Timestamp.Ticks);
            /*
            //add order to order history
            DH.Orders.Add(order);
            //add orderID to user order history
            DH.UserRepo.GetUserByUsername(order.UserID).OrderHistory.Add(order.Id);
            //add orderID to location order history
            DH.Locations.First(l => l.Name.Equals(order.Store)).OrderHistory.Add(order.Id);
            //add order to DB
            */
            //Update decremented inventory to DB
            //DH.LocRepo.UpdateLocationInventory(DH.LocRepo.GetLocationByName(CurOrder.Store));
            //DH.LocRepo.Save();
            DH.OrderRepo.AddOrder(CurOrder);
            DH.LocRepo.Save();

            return null;
        }

        public bool IsOrderSmallEnough()
        {
            if (CurOrder.Pizzas.Count > MaxPizzas)
                return false;
            return true;
        }

        public bool IsOrderCheapEnough()
        {
            if (CurOrder.Price > MaxOrderPrice)
                return false;
            return true;
        }

        public bool IsOrderNotEmpty()
        {
            if (CurOrder.Pizzas.Count <= 0)
                return false;
            return true;
        }

        public bool IsOrderTwoHoursLater(RepositoryHandler DH)
        {
            //Both Users and Locations have an order history contianing order ids
            //Find intersection of User & location order histories from newest to oldest
            //if newest shared order <2 hrs reject, otherwise accept
            List<int> userOrders = DH.UserRepo.GetUserByUsername(CurOrder.UserID).OrderHistory;
            List<int> locationOrders = DH.LocRepo.GetLocations().First(l => l.Name.Equals(CurOrder.Store)).OrderHistory;
            DateTime orderTime;
            //new orders are always added to the end of the OrderHistory list, so go through newest orders first
            for (int i = userOrders.Count-1; i >= 0; i--)
            {
                //get the order time off the user order we want to check
                orderTime = DH.OrderRepo.GetOrderByID(userOrders[i]).Timestamp;
                //if the most recent order being checked is already older than two hours
                //this is just for efficiency to not have to go through a user's entire order history since the beginning of time
                if (DateTime.Compare(DateTime.Now, orderTime.AddHours(2)) > 0)
                {
                    return true;
                }

                 //if an orderID is shared between user and location
                if (locationOrders.Contains(userOrders[i]))
                {
                    orderTime = DH.OrderRepo.GetOrderByID(userOrders[i]).Timestamp;
                    //if order was placed within the last two hours
                    if ( DateTime.Compare(DateTime.Now, orderTime.AddHours(2)) < 0 )
                    {
                        return false;
                    }

                }
            }
            
            return true;
        }

        public bool IsOrderTwoHoursLaterV2(RepositoryHandler DH)
        {
            List<Order> UnionOrders = DH.OrderRepo.GetOrders().Where(o => o.UserID.Equals(CurOrder.UserID) && o.Store.Equals(CurOrder.Store)).ToList();

            foreach (var i in UnionOrders)
            {
                if (DateTime.Compare(DateTime.Now, i.Timestamp.AddHours(2)) < 0)
                {
                    return false;
                }
            }

            return true;
        }

        public string DoesLocationHaveAllIngredients(RepositoryHandler DH)
        {
            //1: generate -List- of all ingredient types w/ appropiate quantity based on scalar
            List<Ingredient> allIngredients = BuildIngredientList(DH);
            return DH.LocRepo.RemoveBulkStockv2(allIngredients, CurOrder.Store);
        }

        public List<Ingredient> BuildIngredientList(RepositoryHandler DH)
        {
            List<Ingredient> allIngredients = new List<Ingredient>();
            int amount;
            foreach (var p in CurOrder.Pizzas)
            {
                amount = DH.SPRepo.GetIngredientUsageScalar(p.Size);
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
