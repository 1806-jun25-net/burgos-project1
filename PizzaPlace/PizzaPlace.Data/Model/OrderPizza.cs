using System;
using System.Collections.Generic;

namespace PizzaPlace.Data
{
    public partial class OrderPizza
    {
        public int OrderPizzaId { get; set; }
        public int? OrderId { get; set; }
        public int? PizzaId { get; set; }
        public int? Quantity { get; set; }

        public Orders Order { get; set; }
        public Pizza Pizza { get; set; }
    }
}
