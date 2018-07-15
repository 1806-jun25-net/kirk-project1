using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.WebApp.Models
{
    public class User
    {

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 2)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15, MinimumLength = 10)]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Preferred Ordering Location:")]
        public string FavStore { get; set; }
        public IEnumerable<SelectListItem> locs { get; set; }
        //public List<int> OrderHistory { get; set; } = new List<int>();
    }
}
