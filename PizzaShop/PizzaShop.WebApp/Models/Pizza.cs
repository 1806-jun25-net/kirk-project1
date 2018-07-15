using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.WebApp.Models
{
    public class Pizza
    {
        [Required]
        [Display(Name = "Pizza Size")]
        public string Size { get; set; }

        [Required]
        [Display(Name = "Crust Type")]
        public string CrustType { get; set; }

        [Required]
        [Display(Name = "Sauce Type")]
        public string SauceType { get; set; }

        [Required]
        [Display(Name = "Toppings")]
        public HashSet<string> Toppings { get; set; }

        public decimal Price { get; set; }

        public Pizza()
        {
            Toppings = new HashSet<String>();
            Size = null;
            CrustType = "classic crust";
            SauceType = "classic sauce";
            Toppings.Add("cheese");
            Price = 0;
        }
    }
}
