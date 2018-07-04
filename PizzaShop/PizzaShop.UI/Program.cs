using PizzaShop.Library;
using System;

namespace PizzaShop.UI
{
    class Program
    {
        private static string userID;
        public static void Main(string[] args)
        {
            //Run deserialization code

            //Start menu
            string input = "";
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
                if (input.Equals("1"))
                    //Create New User
                    MenuNewUser();
                else if (input.Equals("2"))
                    //Existing User Login
                    MenuExistingUserLogin();
                else if (input.Equals("0"))
                    //Exit Application
                    exitApplication = true;
                else
                    //Invalid Input
                    Console.WriteLine("Input invalid.  Please try again.");
            }
            while (exitApplication == false);

            //Run serialization code one final time
        }

        public static void MenuNewUser()
        {
            Console.WriteLine("~~~New User Creation~~~");
            Console.WriteLine("Please create a new username:");
            Console.Write("->");
        }

        public static void MenuExistingUserLogin()
        {
            Console.WriteLine("~~~Existing User Login~~~");
            Console.WriteLine("Please enter your username:");
            Console.Write("->");
            "test".ToUpper();
            //SEARCH FOR USER IN USER TABLE
            // If User found, set user and proceed to menu
            //?????Session object??????? for current user & menu state

            //Else, user not found, prompt to try again

        }
    }
}
