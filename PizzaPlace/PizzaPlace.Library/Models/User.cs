using PizzaPlace.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaPlace.Library.Models
{
    public class User : IUser
    {
        public User(string firstName, string lastName, string phone, bool foundUser)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            FoundUser = foundUser;
        }

        public string FirstName { get; set ; }
        public string LastName { get; set; }
        public string Phone { get; set ; }
        public bool FoundUser { get; set; }
        public string Location { get ; set ; }
    }
}
