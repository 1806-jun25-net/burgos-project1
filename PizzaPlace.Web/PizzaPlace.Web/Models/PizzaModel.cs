using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPlace.Web.Models
{
    public class PizzaModel
    {
        public int PizzaId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public double? Price { get; set; }
    }
}
