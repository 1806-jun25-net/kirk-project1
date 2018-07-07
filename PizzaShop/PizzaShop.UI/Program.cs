﻿using PizzaShop.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PizzaShop.UI
{
    class Program
    {
        private static string userID;
        private static readonly bool readFromXML = false;

        public static void Main(string[] args)
        {
            //Get data ready
            DataAccessor.Setup(readFromXML);

            //Begin at first screen of the menu system
            MenuStart();

            //Run serialization code to back up all changes upon menu termination
            //DataAccessor.SerializeToFile();

        }



        //Menu methods to handle each individual menu state
        public static void MenuStart()
        {
            string input = null;
            bool exitApplication = false;

            do
            {
                //first startup, not yet logged in
                Console.WriteLine("~~~Welcome to the pizza ordering system!~~~");
                Console.WriteLine("Please enter the number for your selection");
                Console.WriteLine("1: New User");
                Console.WriteLine("2: Existing User");
                Console.WriteLine("0: Exit Application");
                Console.Write("->");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":  //Create New User
                        MenuNewUserCreation();
                        break;
                    case "2":  //Existing User Login
                        MenuExistingUserLogin();
                        break;
                    case "0": //Exit Application
                        exitApplication = true;
                        break;
                    default:  //Invalid Input
                        Console.WriteLine("Input invalid.  Please try again.");
                        break;
                }
            }
            while (exitApplication == false);
        }

        public static void MenuNewUserCreation()
        {
            String input = "";
            bool inputValid;
            User newUser = new User();
            
            Console.WriteLine("~~~New User Creation~~~");
            //make username
            do
            {
                Console.WriteLine("Please create a new username:");
                Console.Write("->");
                input = Console.ReadLine();
                inputValid = !DataAccessor.DH.Users.ContainsKey(input)
                    && input.Any(c => char.IsLetter(c));
                if (!inputValid)
                    Console.WriteLine("That username is already taken or is invalid.  Please try again.");
            }
            while (!inputValid);
            newUser.Username = input;

            //get first name
            do
            {
                Console.WriteLine("Please enter first name:");
                Console.Write("->");
                input = Console.ReadLine();
                inputValid = !input.Any(c => char.IsDigit(c)) && input.Any(c => char.IsLetter(c));
                if (!inputValid)
                    Console.WriteLine("That name is invalid.  Names may not contain numbers and must consist of at least 1 letter.  Please try again.");
            }
            while (!inputValid);
            newUser.FirstName = input;
            
            //get last name
            do
            {
                Console.WriteLine("Please enter last name:");
                Console.Write("->");
                input = Console.ReadLine();
                inputValid = !input.Any(c => char.IsDigit(c)) && input.Any(c => char.IsLetter(c));
                if (!inputValid)
                    Console.WriteLine("That name is invalid. Names may not contain numbers and must consist of at least 1 letter.  Please try again.");
            }
            while (!inputValid);
            newUser.LastName = input;

            //get email
            do
            {
                //TODO: use Regex to check for valid email
                Console.WriteLine("Please enter email address:");
                Console.Write("->");
                input = Console.ReadLine();
                inputValid = input != ""
                    && char.IsLetterOrDigit(input[0]) 
                    && input.Count( c => c=='@')==1 
                    && (input.EndsWith(".com") || input.EndsWith(".gov") || input.EndsWith(".edu") || input.EndsWith(".edu") || input.EndsWith(".net"));
                if (!inputValid)
                    Console.WriteLine("That email is invalid. Please try again.");
            }
            while (!inputValid);
            newUser.Email = input;

            //get phone
            do
            {
                //TODO: use regex for valid phone formats (xxx)xxx-xxxx or xxxxxxxxxx
                Console.WriteLine("Please enter phone number: (digits only, no (, ), or -");
                Console.Write("->");
                input = Console.ReadLine();
                inputValid = input.Length == 10 && input.Count(c => !char.IsDigit(c)) == 0;
                    
                if (!inputValid)
                    Console.WriteLine("That phone number is invalid. Enter phone number as 10 digits only.  Please try again.");
            }
            while (!inputValid);
            newUser.Phone = input;

            //get fav store
            do
            {
                PrintLocations();
                Console.WriteLine("Please select your default store by number:");
                Console.Write("->");
                input = Console.ReadLine();
                inputValid = input.Count(c => !char.IsDigit(c)) == 0 
                    && Int32.Parse(input) >= 1 
                    && Int32.Parse(input) <= DataAccessor.DH.Locations.Count;
                if (!inputValid)
                    Console.WriteLine("That selection is invalid.  Enter only the number associated with the store. Please try again.");
            }
            while (!inputValid);
            newUser.FavStore = DataAccessor.DH.Locations[Int32.Parse(input)-1].Name;

            Console.WriteLine("New user created!");
            Console.WriteLine($"Username: {newUser.Username}\nFirst Name: {newUser.FirstName}\nLast Name: {newUser.LastName}\nEmail: {newUser.Email}\nPhone: {newUser.Phone}\nDefault Location: {newUser.FavStore}");

            //add new user to user list
            DataAccessor.DH.Users.Add(newUser.Username, newUser);
        }

        public static void MenuExistingUserLogin()
        {
            string input = "";
            string input2 = "";
            do {
                Console.WriteLine("~~~Existing User Login~~~");
                Console.WriteLine("Please enter your username:");
                Console.Write("->");
                input = Console.ReadLine();
                if (!DataAccessor.DH.Users.ContainsKey(input))
                {
                    do
                    {
                        Console.WriteLine($"User \"{input}\"not recognized!");
                        Console.WriteLine("1: Try again");
                        Console.WriteLine("2: Go back");
                        input2 = Console.ReadLine();
                    }
                    while (!(input2.Equals("1") || input2.Equals("2")));
                }
                else
                {
                    userID = input;
                    input2 = "2";
                    MenuMain();
                }
            }
            while (userID == null && !input2.Equals("2"));
            //SEARCH FOR USER IN USER TABLE
            // If User found, set user and proceed to menu
            //?????Session object??????? for current user & menu state

            //Else, user not found, prompt to try again

        }

        public static void MenuMain()
        {
            string input = "";
            bool exitMenu = false;
            Console.WriteLine($"Welcome back, {userID}!");
            do
            {
                Console.WriteLine("~~~Main Menu~~~");
                Console.WriteLine("Please enter the number for your selection");
                Console.WriteLine("1: Start a New Order");
                Console.WriteLine("2: Order Archive");
                Console.WriteLine("3: Location, Inventory, & Menu Management");
                Console.WriteLine("0: Logout");
                Console.Write("->");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":  //Start a New Order
                        MenuNewOrderCreation();
                        break;
                    case "2":  //Order Archive
                        MenuOrderArchive();
                        break;
                    case "3":  //Location, Inventory, & Menu Management
                        MenuLocationInventoryMenuManagement();
                        break;
                    case "0": //Logout
                        exitMenu = true;
                        break;
                    default:  //Invalid Input
                        Console.WriteLine("Input invalid.  Please try again.");
                        break;
                }

            }
            while (!exitMenu);
            userID = null;
            Console.WriteLine("You have successfully logged out.");
        }

        public static void MenuNewOrderCreation()
        {
            string input = "";
            bool invalidInput = true;
            List<IPizza> recommendedOrder = DataAccessor.DH.Users[userID].GetRecommendedOrder();
            //OrderBuilder ob;
            Console.WriteLine("~~~New Order Creation~~~");
            Console.WriteLine("Check out this recommended order, just for you!");
            PrintPizzaList(recommendedOrder);
            do
            {
                Console.WriteLine("Please enter the number for your selection");
                Console.WriteLine("1: Start from recommended order");
                Console.WriteLine("2: Start a new empty order");
                Console.WriteLine("0: Go Back");
                Console.Write("->");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":  //recommended order
                        MenuBuildOrder(new OrderBuilder(userID, DataAccessor.DH.Users[userID].FavStore, recommendedOrder));
                        invalidInput = false;
                        break;
                    case "2":  //new empty order
                        MenuBuildOrder(new OrderBuilder(userID, DataAccessor.DH.Users[userID].FavStore));
                        invalidInput = false;
                        break;
                    case "0": //Go back
                        invalidInput = false;
                        break;
                    default:  //Invalid Input
                        Console.WriteLine("Input invalid.  Please try again.");
                        break;
                }
            }
            while (invalidInput);
        }

        public static void MenuBuildOrder(OrderBuilder ob)
        {
            string input = "";
            bool exitMenu = false;
            do
            {
                if (ob.GetPizzas().Count == 0)
                    Console.WriteLine("Your order is currently empty.");
                else
                {
                    Console.WriteLine("Your current order:");
                    Console.WriteLine($"Order location: {ob.order.Store}");
                    PrintPizzaList(ob.GetPizzas());
                    Console.WriteLine($"---------------------\n   Total Price: ${ob.CalculateTotalPrice()}\n");
                }

                Console.WriteLine("Please enter the number for your selection");
                Console.WriteLine("1: Add a new pizza to order");
                Console.WriteLine("2: Duplicate a pizza already in your order");
                Console.WriteLine("3: Modify a pizza in your order");
                Console.WriteLine("4: Remove a pizza from your order");
                Console.WriteLine("5: Change location");
                Console.WriteLine("6: Place order");
                Console.WriteLine("0: Cancel order");
                Console.Write("->");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":  //add new pizza
                        MenuAddNewPizza(ob);
                        break;
                    case "2":  // duplicate pizza
                        MenuDuplicatePizza(ob);
                        break;
                    case "3":  //modify pizza
                        MenuModifyExistingPizza(ob);
                        break;
                    case "4":  //remove pizza from order
                        MenuRemovePizza(ob);
                        break;
                    case "5":  //change location
                        MenuChangeLocation(ob);
                        break;
                    case "6":  //(attempt to) place order
                        if (MenuFinalizeOrder(ob))
                            exitMenu = true;
                        break;
                    case "0": //Go back
                        exitMenu = true;
                        break;
                    default:  //Invalid Input
                        Console.WriteLine("Input invalid.  Please try again.");
                        break;
                }
            }
            while (!exitMenu);


        }

        public static void MenuAddNewPizza(OrderBuilder ob)
        {
            string input = "";
            bool validInput = true;

            Console.WriteLine("Select your pizza's size:");
            PrintPizzaSizes();
            do
            {
                Console.WriteLine("->");
                input = Console.ReadLine();
                if (input.Any(c => !char.IsDigit(c))
                    || Int32.Parse(input) < 1
                    || Int32.Parse(input) > DataAccessor.DH.SPM.Sizes.Count)
                {
                    validInput = false;
                    Console.WriteLine("Input invalid.  Please try again.");
                }
                else
                    validInput = true;
            }
            while (!validInput);
            ob.StartNewPizza(DataAccessor.DH.SPM.Sizes[Int32.Parse(input)-1]);
            //new pizza created, now allow for topping/sauce/crust changes
            MenuModifyPizza(ob);
            ob.AddActivePizza();




        }
            
        public static void MenuDuplicatePizza(OrderBuilder ob)
        {
            string input = "";
            bool inputValid = false;
            do
            {
                MenuHelperSelectPizza("duplicate", "pizza", ob.order.Pizzas);
                Console.Write("->");
                input = Console.ReadLine();
                inputValid = input.Count(c => !char.IsDigit(c)) == 0
                    && Int32.Parse(input) >= 1
                    && Int32.Parse(input) <= ob.order.Pizzas.Count;
                if (!inputValid)
                    Console.WriteLine("That selection is invalid.  Enter only the number associated with the pizza you wish to duplicate. Please try again.");
            }
            while (!inputValid && !input.Equals("0"));
            var targetPizza = ob.order.Pizzas[Int32.Parse(input) - 1];
            var newPizza = new BuildYourOwnPizza(targetPizza.Size);
            newPizza.CrustType = targetPizza.CrustType;
            newPizza.SauceType = targetPizza.SauceType;
            foreach (var t in targetPizza.Toppings)
                newPizza.Toppings.Add(t);
            newPizza.Price = targetPizza.Price;
            ob.AddPizza(newPizza);
        }

        public static void MenuModifyExistingPizza(OrderBuilder ob)
        {
            string input = "";
            bool inputValid = false;
            do
            {
                MenuHelperSelectPizza("modify", "pizza", ob.order.Pizzas);
                Console.Write("->");
                input = Console.ReadLine();
                inputValid = input.Count(c => !char.IsDigit(c)) == 0
                    && Int32.Parse(input) >= 1
                    && Int32.Parse(input) <= ob.order.Pizzas.Count;
                if (!inputValid)
                    Console.WriteLine("That selection is invalid.  Enter only the number associated with the pizza you wish to duplicate. Please try again.");
            }
            while (!inputValid && !input.Equals("0"));
            if (!input.Equals("0"))
            {
                ob.SwitchActivePizza(Int32.Parse(input) - 1);
                MenuModifyPizza(ob);
            }

        }

        public static void MenuRemovePizza(OrderBuilder ob)
        {
            string input = "";
            bool inputValid = false;
            do
            {
                MenuHelperSelectPizza("remove", "pizza", ob.order.Pizzas);
                Console.Write("->");
                input = Console.ReadLine();
                inputValid = input.Count(c => !char.IsDigit(c)) == 0
                    && Int32.Parse(input) >= 1
                    && Int32.Parse(input) <= ob.order.Pizzas.Count;
                if (!inputValid)
                    Console.WriteLine("That selection is invalid.  Enter only the number associated with the pizza you wish to remove. Please try again.");
            }
            while (!inputValid && !input.Equals("0"));
            ob.RemovePizza(Int32.Parse(input)-1);
        }

        public static void MenuChangeLocation(OrderBuilder ob)
        {
            string input = "";
            bool inputValid = false;
            do
            {
                PrintLocations();
                Console.WriteLine("Please select your default store by number, or 0 to go back without changing:");
                Console.Write("->");
                input = Console.ReadLine();
                inputValid = input.Count(c => !char.IsDigit(c)) == 0
                    && Int32.Parse(input) >= 1
                    && Int32.Parse(input) <= DataAccessor.DH.Locations.Count;
                if (!inputValid)
                    Console.WriteLine("That selection is invalid.  Enter only the number associated with the store. Please try again.");
            }
            while (!inputValid  && !input.Equals("0"));
            if(!input.Equals("0"))
            {
                ob.ChangeLocation(DataAccessor.DH.Locations[Int32.Parse(input)-1].Name);
            }
        }

        public static void MenuModifyPizza(OrderBuilder ob)
        {
            string input = "";
            bool exitMenu = false;

            do
            {
                PrintPizza(ob.ActivePizza);
                Console.WriteLine("Please enter the number for your selection");
                Console.WriteLine("1: Add topping to pizza");
                Console.WriteLine("2: Remove topping from pizza");
                Console.WriteLine("3: Change crust");
                Console.WriteLine("4: Change sauce");
                Console.WriteLine("5: Change size");
                Console.WriteLine("6: Add pizza to order");
                Console.Write("->");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":  //AddTopping
                        MenuAddToppings(ob);
                        break;
                    case "2":  //RemoveTopping
                        MenuRemoveToppings(ob);
                        break;
                    case "3":  //chance crust
                        MenuChangePizzaCrust(ob);
                        break;
                    case "4":  //change sauce
                        MenuChangePizzaSauce(ob);
                        break;
                    case "5":  //change size
                        MenuChangePizzaSize(ob);
                        break;
                    case "6":  //add to order
                        exitMenu = true;
                        break;
                    default:  //Invalid Input
                        Console.WriteLine("Input invalid.  Please try again.");
                        break;
                }

            }
            while (!exitMenu);
        }

        public static void MenuAddToppings(OrderBuilder ob)
        {
            string input = "";
            bool exitMenu = false;
            bool result;
            do
            {
                MenuHelperSelectIngredient("add", "topping", DataAccessor.DH.ingDir.Toppings);
                input = Console.ReadLine();
                result = ob.AddToppingToActivePizza(input);
                if (result)
                {
                    Console.WriteLine($"The topping '{input}' has been added to your pizza.");
                    PrintPizza(ob.ActivePizza);
                }
                else if (input.Equals("0"))
                    exitMenu = true;
                else
                    Console.WriteLine($"We did not recognize the topping '{input}', or it already is on your pizza.  It could not be added to your pizza.");
            }
            while (!exitMenu);
        }

        public static void MenuRemoveToppings(OrderBuilder ob)
        {
            string input = "";
            bool exitMenu = false;
            bool result;
            do
            {
                MenuHelperSelectIngredient("remove", "topping", ob.ActivePizza.Toppings);
                input = Console.ReadLine();
                result = ob.RemoveToppingFromActivePizza(input);
                if (result)
                {
                    Console.WriteLine($"The topping '{input}' has been removed from your pizza.");
                    PrintPizza(ob.ActivePizza);
                }
                else if (input.Equals("0"))
                    exitMenu = true;
                else
                    Console.WriteLine($"We did not recognize the topping '{input}', or it is not currently on your pizza.  It could not be removed from your pizza.");
            }
            while (!exitMenu);
        }

        public static void MenuChangePizzaCrust(OrderBuilder ob)
        {
            string input = "";
            bool exitMenu = false;
            bool result;
            do
            {
                MenuHelperSelectIngredient("change to", "crust", DataAccessor.DH.ingDir.Crusts);
                input = Console.ReadLine();
                result = ob.ChangeCrustOnActivePizza(input);
                if (result)
                {
                    Console.WriteLine($"The crust '{input}' has been set for your pizza.");
                    PrintPizza(ob.ActivePizza);
                }
                else if (input.Equals("0"))
                    exitMenu = true;
                else
                    Console.WriteLine($"We did not recognize the crust '{input}'.  The pizza crust has not been changed.");
            }
            while (!exitMenu);
        }

        public static void MenuChangePizzaSauce(OrderBuilder ob)
        {
            string input = "";
            bool exitMenu = false;
            bool result;
            do
            {
                MenuHelperSelectIngredient("change to", "sauce", DataAccessor.DH.ingDir.Sauces);
                input = Console.ReadLine();
                result = ob.ChangeSauceOnActivePizza(input);
                if (result)
                {
                    Console.WriteLine($"The sauce '{input}' has been set for your pizza.");
                    PrintPizza(ob.ActivePizza);
                }
                else if (input.Equals("0"))
                    exitMenu = true;
                else
                    Console.WriteLine($"We did not recognize the sauce '{input}'.  The pizza sauce has not been changed.");
            }
            while (!exitMenu);
        }

        public static void MenuChangePizzaSize(OrderBuilder ob)
        {
            string input = "";
            bool exitMenu = false;
            bool result;
            do
            {
                MenuHelperSelectIngredient("change to", "size", DataAccessor.DH.SPM.Sizes);
                input = Console.ReadLine();
                result = ob.ChangeSizeOfActivePizza(input);
                if (result)
                {
                    Console.WriteLine($"The size '{input}' has been set for your pizza.");
                    PrintPizza(ob.ActivePizza);
                }
                else if (input.Equals("0"))
                    exitMenu = true;
                else
                    Console.WriteLine($"We did not recognize the size '{input}'.  The pizza size has not been changed.");
            }
            while (!exitMenu);
        }

        public static bool MenuFinalizeOrder(OrderBuilder ob)
        {
            string result;
            Console.WriteLine("Finalizing Order...");
            result = ob.FinalizeOrder();
            if (result == null)
            {
                Console.WriteLine($"Order successfully placed!  Your order's ID is: {ob.order.Id}");
                return true;
            }
            else
            {
                Console.WriteLine(result);
                return false;
            }
        }

        public static void MenuOrderArchive()
        {
            
            string input = "";
            bool exitMenu = false;
            Console.WriteLine($"Welcome back, {userID}!");
            do
            {
                Console.WriteLine("~~~Order Archive~~~");
                Console.WriteLine("Please enter the number for your selection");
                Console.WriteLine("1: Single Order Lookup by ID");
                Console.WriteLine("2: User Order History");
                Console.WriteLine("3: Location Order History");
                Console.WriteLine("4: All Orders");
                Console.WriteLine("0: Go Back");
                Console.Write("->");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":  // single order by ID
                        MenuOrdersByID();
                        break;
                    case "2":  // orders by current user
                        MenuOrdersByUser();
                        break;
                    case "3":  // orders by location
                        MenuOrdersByLocation();
                        break;
                    case "4":  // all orders menu
                        MenuAllOrdersSorting();
                        break;
                    case "0": // go back
                        exitMenu = true;
                        break;
                    default:  //Invalid Input
                        Console.WriteLine("Input invalid.  Please try again.");
                        break;
                }

            }
            while (!exitMenu);
        }

        public static void MenuOrdersByID()
        {
            string input = "";
            Console.WriteLine("Please enter the order ID you wish to view:");
            Console.Write("->");
            input = Console.ReadLine();
            if (!DataAccessor.DH.Orders.ContainsKey(input))
                Console.WriteLine("OrderID not recognized.");
            else
            {
                PrintOrder(DataAccessor.DH.Orders[input]);
            }
        }

        public static void MenuOrdersByUser()            //TODO once SQL is up and running   for now prints all orders
        {
            Console.WriteLine("This functionality is under construction. Press enter to proceed with caution");
            Console.ReadLine();



            string input = "";
            Console.WriteLine("Please enter the username who's orders you wish to view:");
            Console.Write("->");
            input = Console.ReadLine();
            //if (!DataAccessor.DH.Orders.ContainsKey(input))
            //    Console.WriteLine("Input not recognized.");

            //else
            {
                foreach (KeyValuePair<string, IOrder> entry in DataAccessor.DH.Orders)
                {
                    PrintOrder(entry.Value);
                }
            }
        }

        public static void MenuOrdersByLocation()            //TODO once SQL is up and running
        {
            Console.WriteLine("This functionality is under construction. Press enter to proceed with caution");
            Console.ReadLine();



            string input = "";
            bool inputValid = false;
            do
            {
                PrintLocations();
                Console.WriteLine("Please select the location by number to see its orders, or 0 to go back without viewing:");
                Console.Write("->");
                input = Console.ReadLine();
                inputValid = input.Count(c => !char.IsDigit(c)) == 0
                    && Int32.Parse(input) >= 1
                    && Int32.Parse(input) <= DataAccessor.DH.Locations.Count;
                if (!inputValid)
                    Console.WriteLine("That selection is invalid.  Enter only the number associated with the store. Please try again.");
            }
            while (!inputValid && !input.Equals("0"));
            if (!input.Equals("0"))
            {

                {
                    foreach (KeyValuePair<string, IOrder> entry in DataAccessor.DH.Orders)
                    {
                        PrintOrder(entry.Value);
                    }
                }
            }
        }

        public static void MenuAllOrdersSorting()            //TODO once SQL is up and running
        {
            Console.WriteLine("This functionality is under construction. Press enter to proceed with caution");
            Console.ReadLine();
        }

        public static void MenuLocationInventoryMenuManagement()
        {
            Console.WriteLine("~~~Location, Inventory, & Menu Management~~~");
            Console.WriteLine("This functionality is under construction. Press enter to proceed with caution");
            Console.ReadLine();
        }



        //MenuHelper methods perform tasks for our Methods like prompting for input but do not control menu flow
        public static void MenuHelperSelectIngredient(string action, string type, IEnumerable<IIngredient> ingredients)
        {
            Console.WriteLine($"Please type the name of the {type} you would like to {action}, or 0 to go back:");
            PrintIngredients(ingredients);
        }
        public static void MenuHelperSelectIngredient(string action, string type, IEnumerable<string> ingredients)
        {
            Console.WriteLine($"Please type the name of the {type} you would like to {action}, or 0 to go back:");
            PrintIngredients(ingredients);
        }
        public static void MenuHelperSelectPizza(string action, string type, List<IPizza> ingredients)
        {
            Console.WriteLine($"Please type the number of the {type} you would like to {action}, or 0 to go back:");
            PrintPizzaList(ingredients);
        }


        // Printing methods to streamline menu method code:
        public static void PrintPizzaList(List<IPizza> pizzas)
        { 
            for(int i = 0; i < pizzas.Count; i++)
            {
                Console.Write($"Pizza {i + 1}:  ");
                PrintPizza(pizzas[i]);
                Console.WriteLine();
            }
        }

        public static void PrintPizza(IPizza pizza)
        {
            Console.Write($"{pizza.Size} pizza, {pizza.CrustType}, {pizza.SauceType}\n    Toppings:");
            PrintIngredients(pizza.Toppings);
            Console.WriteLine($"    Price: ${pizza.Price}");
        }

        public static void PrintIngredients(IEnumerable<IIngredient> ingredients)
        {
            foreach (IIngredient t in ingredients)
            {
                Console.Write($" {t.Name},");
            }
        }

        public static void PrintIngredients(IEnumerable<string> ingredients)
        {
            foreach (string t in ingredients)
            {
                Console.Write($" {t},");
            }
            Console.WriteLine();
        }

        public static void PrintPizzaSizes()
        {
            for(int i = 0; i < DataAccessor.DH.SPM.Sizes.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {DataAccessor.DH.SPM.Sizes[i]}");
            }
        }

        public static void PrintLocations()
        {
            Console.WriteLine("Our order locations are:");
            for (int i = 0; i < DataAccessor.DH.Locations.Count; i++)
                Console.WriteLine($"{i + 1}: {DataAccessor.DH.Locations[i].Name}");
        }

        public static void PrintOrder(IOrder order)
        {
            Console.WriteLine($"Order ID: {order.Id}");
            Console.WriteLine($"Order Prpared for username: {order.UserID}");
            Console.WriteLine($"Order location: {order.Store}");
            PrintPizzaList(order.Pizzas);
            Console.WriteLine($"---------------------\n   Total Price: ${order.Price}\n");
            Console.WriteLine($"Order placed on: {order.Timestamp}");
        }

    }
}
