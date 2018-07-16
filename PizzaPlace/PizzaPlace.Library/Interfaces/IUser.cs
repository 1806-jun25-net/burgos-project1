using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaPlace.Library.Interfaces
{
    interface IUser
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Phone { get; set; }
        bool FoundUser { get; set; }
        string Location { get; set; }
    }
}
