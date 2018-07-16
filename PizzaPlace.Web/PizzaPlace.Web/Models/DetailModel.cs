using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPlace.Web.Models
{
    public class DetailModel
    {
        public IEnumerable<UsersModel> User { get; set; }
        public IEnumerable<OrdersPizzaModel> OrderPizza { get; set; }
        public IEnumerable<OrdersModel> Orders { get; set; }
        public IEnumerable<PizzaModel> Pizza { get; set; }

        public int Counter { get; set; } = 1;
        public SelectListItem SelectTopping { get; set; }
        public SelectListItem SelectSize { get; set; }
    }
}
