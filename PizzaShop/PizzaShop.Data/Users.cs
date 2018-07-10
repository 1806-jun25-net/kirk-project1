using System;
using System.Collections.Generic;

namespace PizzaShop.Data
{
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
        }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FavLocation { get; set; }

        public Locations FavLocationNavigation { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
