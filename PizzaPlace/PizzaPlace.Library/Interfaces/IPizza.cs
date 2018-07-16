using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaPlace.Library.Interfaces
{
    interface IPizza
    {
        string Name { get; set; }
        string Size { get; set; }
        double Price { get; set; }
    }
}
