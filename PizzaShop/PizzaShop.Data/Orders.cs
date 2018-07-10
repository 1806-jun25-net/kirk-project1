using System;
using System.Collections.Generic;

namespace PizzaShop.Data
{
    public partial class Orders
    {
        public Orders()
        {
            OrderPizzaJunction = new HashSet<OrderPizzaJunction>();
        }

        public int Id { get; set; }
        public DateTime? Timestamp { get; set; }
        public string LocationId { get; set; }
        public string UserId { get; set; }
        public decimal? Price { get; set; }

        public Locations Location { get; set; }
        public Users User { get; set; }
        public ICollection<OrderPizzaJunction> OrderPizzaJunction { get; set; }
    }
}
