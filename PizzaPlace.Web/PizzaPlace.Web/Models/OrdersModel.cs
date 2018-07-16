using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPlace.Web.Models
{
    public class OrdersModel
    {
        public int OrderId { get; set; }
        public double? Total { get; set; }
        public int? LocationId { get; set; }
        public int? UsersId { get; set; }
        public DateTime? OrderTime { get; set; }

        
    }
}
