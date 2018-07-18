using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaPlace.DataAccess
{
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
        }

        public int UsersId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        public int? LocationId { get; set; }

        public Locations Location { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
