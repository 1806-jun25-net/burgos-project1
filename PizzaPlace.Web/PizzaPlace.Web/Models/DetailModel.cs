using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaPlace.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPlace.Web.Models
{
    public class DetailModel
    {
        public IEnumerable<Users> User { get; set; }
        public IEnumerable<OrderPizza> OrderPizza { get; set; }
        public IEnumerable<Orders> Orders { get; set; }
        public IEnumerable<Pizza> Pizza { get; set; }

        public int Counter { get; set; } = 1;

        [Required]
        public SelectListItem SelectTopping { get; set; }

        [Required]
        public SelectListItem SelectSize { get; set; }
    }
}
