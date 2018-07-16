using Microsoft.EntityFrameworkCore;
using PizzaPlace.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlace.Library
{
    public class UserRepository
    {
        private readonly PizzaPlaceDBContext _db;


        public UserRepository(PizzaPlaceDBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


        }

        public int? GetUserIDByPhone(string findUser, string phone)
        {

            var user = _db.Users.FirstOrDefault(g => g.FirstName == findUser && g.Phone == phone);
            if (user == null)
            {
                return 0;
            }

            else
            {
                return user.UsersId;
            }


        }


        public string GetUser(string findUser)
        {
            var user = _db.Users.FirstOrDefault(g => g.FirstName == findUser);
            if (user == null)
            {

                Console.WriteLine("No user found");
                Console.ReadLine();
                return "";
                
            }
            else
            {
                string Name =  user.FirstName + " " + user.LastName;
                return Name;
            }
        }

        public bool GetUserByPhone(string findUser, string phone)
        {

            var user = _db.Users.FirstOrDefault(g => g.FirstName == findUser && g.Phone == phone);
            if (user == null)
            {
                return false;
            }

            else
            {
                return true;
            }
            

        }



        public void AddUsers(string firstName, string lastName, string phone, int location)
        {


            var users = new Users
            {
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
                LocationId = location
            };
            _db.Add(users);
        }

        public void Edit(Users user)
        {
            //uodates the current user 
            _db.Update(user);
            
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

    }
}
