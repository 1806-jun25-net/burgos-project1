using System;
using System.Collections.Generic;

namespace PizzaPlace.Data
{
    public partial class Pizza
    {
        public Pizza()
        {
            OrderPizza = new HashSet<OrderPizza>();
        }

        public int PizzaId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public double? Price { get; set; }

        public ICollection<OrderPizza> OrderPizza { get; set; }
    }
}
