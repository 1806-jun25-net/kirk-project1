using System;

namespace PizzaShop.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            //Main menu logic all going here for now
            do
            {
                //first startup, not yet logged in
                Console.WriteLine("Welcome to the pizza ordering system!");
                Console.WriteLine("Please enter your username, or press ender to create a new user.");
                input = Console.ReadLine();
                if (input.Equals(""))
                {
                    //CREATE NEW USER
                }
                else
                {
                    //SEARCH FOR USER IN USER TABLE
                    // If User found, set user and proceed to menu
                    //?????Session object??????? for current user & menu state

                    //Else, user not found, prompt to try again
                }
            }
            while (true);
        }
    }
}
