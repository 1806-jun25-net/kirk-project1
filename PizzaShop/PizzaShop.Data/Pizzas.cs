using System;
using System.Collections.Generic;

namespace PizzaShop.Data
{
    public partial class Pizzas
    {
        public Pizzas()
        {
            OrderPizzaJunction = new HashSet<OrderPizzaJunction>();
            PizzaIngredientJunction = new HashSet<PizzaIngredientJunction>();
        }

        public int Id { get; set; }
        public string SizeId { get; set; }
        public decimal? Price { get; set; }

        public SizingPricing Size { get; set; }
        public ICollection<OrderPizzaJunction> OrderPizzaJunction { get; set; }
        public ICollection<PizzaIngredientJunction> PizzaIngredientJunction { get; set; }
    }
}
