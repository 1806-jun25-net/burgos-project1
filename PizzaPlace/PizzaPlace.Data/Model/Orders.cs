using System;
using System.Collections.Generic;

namespace PizzaPlace.Data
{
    public partial class Orders
    {
        public Orders()
        {
            OrderPizza = new HashSet<OrderPizza>();
        }

        public int OrderId { get; set; }
        public double? Total { get; set; }
        public int? LocationId { get; set; }
        public int? UsersId { get; set; }
        public DateTime? OrderTime { get; set; }

        public Locations Location { get; set; }
        public Users Users { get; set; }
        public ICollection<OrderPizza> OrderPizza { get; set; }
    }
}
