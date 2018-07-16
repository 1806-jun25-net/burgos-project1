using System;
using System.Collections.Generic;

namespace PizzaPlace.Data
{
    public partial class HasToppings
    {
        public int ToppingsId { get; set; }
        public int? PizzaId { get; set; }
        public int? LocationId { get; set; }

        public Inventory Location { get; set; }
    }
}
