using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    public interface IUser
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }  
        string Phone { get; set; }
        string FavStore { get; set; }
        string Username { get; set; }


        List<IPizza> GetRecommendedOrder();
    }
}
