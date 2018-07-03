using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public class User : IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FavStore { get; set; }
        public string Username { get; set; }

    }
}
