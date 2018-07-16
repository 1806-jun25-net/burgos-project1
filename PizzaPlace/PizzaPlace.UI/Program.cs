using System;
using System.Collections.Generic;
using PizzaPlace.Data;
using PizzaPlace.Library;
using PizzaPlace.Library.Interfaces;
using PizzaPlace.Library.Models;
using PizzaPlace.Library.Repositories;

namespace PizzaPlace.UI
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            //var repo = new OrdersRepository(new PizzaPlaceDBContext());
            //Console.WriteLine("Enter Name");
            //string FirstName = Console.ReadLine();
            //Console.WriteLine("Enter LastName");
            //string LastName = Console.ReadLine();

            //int location_id = 1;
            //var locations = repo.GetLocationOrderEarliest(location_id); //repo.GetLocationOrders(location_id);
            //foreach (var item in locations)
            //{
            //    Console.WriteLine("Location: 1 " + " Order No. " + item.OrderId + " \n Order Date & time: " + item.OrderTime + "\n Order total: " + item.Total);
            //}
            //Console.ReadLine();

            //repo.SugestedOrder("Kevin", "Sanchez Burgos");
            //repo.GetUserOrders("Kevin", "Sanchez Burgos");



            //Creating objects
            Location Loc = new Location();
            Order newUser = new Order();
            List<User> users = new List<User>();
            var repo = new UserRepository(new PizzaPlaceDBContext());

            //Start the app
            Console.WriteLine("-----------------------------\n\n" +
                "*****Welcome to PR Pizza*****\n\n" +
                "-----------------------------");
            Console.WriteLine("Enter your Name:");
            string FirstName = Console.ReadLine();
            Console.WriteLine("Enter your LastName:");
            string LastName = Console.ReadLine();
            Console.WriteLine("Enter your phone number:");
            string Phone = Console.ReadLine();


            //Set default location
            int location = 1;

            bool foundUser = false;

            foundUser = repo.GetUserByPhone(FirstName, Phone);

            if (foundUser == false)
            {
                users.Add(new User(FirstName, LastName, Phone, foundUser));


                Console.WriteLine("--------------------------------\n\n" +
                $"*****Welcome {FirstName}*****\n\n" +
                "--------------------------------\n");
                Console.ReadLine();

                //Inserts new user into table Users
                newUser.CreateUser(users, location);


                //Calls the Location Menu
                Loc.KevinPizzaMenu(users, location);
            }
            else
            {
                users.Add(new User(FirstName, LastName, Phone, foundUser));

                Console.WriteLine("--------------------------------\n\n" +
                $"*****Welcome Back {FirstName}*****\n\n" +
                "--------------------------------\n");
                Console.ReadLine();
                //Calls the Location Menu
                Loc.KevinPizzaMenu(users, location);
            }

        }
        
        
    }

}
