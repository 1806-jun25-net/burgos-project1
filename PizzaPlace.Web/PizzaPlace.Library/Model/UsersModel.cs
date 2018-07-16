using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaPlace.DataAccess
{
    public partial class UsersModel
    {
        public int UsersId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }

        public int? LocationId { get; set; }

        
    }
}
