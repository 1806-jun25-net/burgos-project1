using System;
using System.Collections.Generic;

namespace PizzaPlace.DataAccess
{
    public partial class Locations
    {
        public Locations()
        {
            Orders = new HashSet<Orders>();
            Users = new HashSet<Users>();
        }

        public int LocationId { get; set; }
        public string Name { get; set; }

        public Inventory Inventory { get; set; }
        public ICollection<Orders> Orders { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
