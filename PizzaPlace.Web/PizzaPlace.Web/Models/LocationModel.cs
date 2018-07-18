using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPlace.Web.Models
{
    public class LocationModel
    {
        public int LocationId { get; set; }

        [Required]
        public string Name { get; set; }

        public SelectListItem SelectedLocation { get; set; }

    }
}
