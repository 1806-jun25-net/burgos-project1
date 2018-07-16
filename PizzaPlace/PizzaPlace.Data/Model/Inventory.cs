using System;
using System.Collections.Generic;

namespace PizzaPlace.Data
{
    public partial class Inventory
    {
        public Inventory()
        {
            HasToppings = new HashSet<HasToppings>();
        }

        public int LocationId { get; set; }
        public int? Cheese { get; set; }
        public int? Pepperoni { get; set; }
        public int? Sausage { get; set; }
        public int? Bacon { get; set; }
        public int? Onions { get; set; }
        public int? Chicken { get; set; }
        public int? Chorizo { get; set; }
        public int? Dough { get; set; }

        public Locations Location { get; set; }
        public ICollection<HasToppings> HasToppings { get; set; }
    }
}
