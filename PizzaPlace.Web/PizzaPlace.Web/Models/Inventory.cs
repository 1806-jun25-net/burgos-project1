using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPlace.Web.Models
{
    public class Inventory
    {
        public int LocationId { get; set; }
        public int? Cheese { get; set; }
        public int? Pepperoni { get; set; }
        public int? Sausage { get; set; }
        public int? Bacon { get; set; }
        public int? Onions { get; set; }
        public int? Chicken { get; set; }
        public int? Chorizo { get; set; }
        public int? Dough { get; set; }

    }
}
