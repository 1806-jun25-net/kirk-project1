using PizzaShop.Library;
using System;
using System.Linq;

namespace PizzaShop.UI
{
    class Program
    {
        private static string userID;
        public static void Main(string[] args)
        {
            //Run deserialization code

            //Start menu
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
                if (input.Equals("1"))
                    //Create New User
                    MenuNewUserCreation();
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
                inputValid = !DataAccessor.DH.Users.ContainsKey(input);
                if (!inputValid)
                    Console.WriteLine("That username is already taken.  Please try again.");
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
            newUser.Email = input;

            //get fav store

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
                if (!DataAccessor.DH.Users.ContainsKey("input"))
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

        }
    }
}
