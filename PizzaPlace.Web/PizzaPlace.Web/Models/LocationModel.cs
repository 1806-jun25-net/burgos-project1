using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPlace.Web.Models
{
    public class LocationModel
    {
        public int LocationId { get; set; }
        public string Name { get; set; }

        public SelectListItem selectedLocation { get; set; }

    }
}
