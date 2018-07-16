using PizzaPlace.Data;
using PizzaPlace.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaPlace.Library.Models
{
    public class Location
    {
        
        public string Name { get; set; }

        //Object
        private Pizza pizza = new Pizza();


        //Methods:
        //------------------------------------------------------------

        //Location Menu
        public void MenuLocation(List<User> users, int location)
        {
            Console.Clear();

            Console.WriteLine(
                "-----------------------------\n\n" +
                "*****Welcome to PR Pizza*****\n\n" +
                "-----------------------------\n" +
                "-----Select the location you wanna order From----\n\n" +
                "1) Kevin's Pizza \n" +
                "2) Pizzageddon  \n" +
                "Please choose one of the options above: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    location = 1;
                    KevinPizzaMenu(users, location);
                    break;

                case "2":
                    location = 2;
                    PizzageddonMenu(users, location);
                    break;

                default:
                    Console.WriteLine("Sorry, invalid selection");
                    Console.ReadLine();
                    MenuLocation(users, location);
                    break;
            }

        }

       
        //-----------------------------------------------------------------

        //KevinPizza Location Menu
        public void KevinPizzaMenu(List<User> users, int location)
        {
            string quantity;
            int setQuantity;
            Choose:
            Console.Clear();
            

            Console.WriteLine("-----------------------------\n\n" +
                "*****Welcome to Kevin's Pizza*****\n\n" +
                "-----------------------------\n" +
                "1) Order Pizza!\n" +
                "2) Change Location.\n" +
                "3) Pizza Storaged Orders Menu\n" +
                "4) Exit Application.\n" +
                "Please choose one of the options above: ");
            

            string choice = Console.ReadLine();
            int count = 1;

            switch (choice)
            {
                case "1":
                    ChooseAgain:
                    Console.WriteLine("How Many Pizza you want to order?");
                    quantity = Console.ReadLine();
                    setQuantity = Int32.Parse(quantity);
                    if(setQuantity == 0)
                    {
                        Console.WriteLine("Please choose to make at least 1 order.");
                        goto ChooseAgain;
                    }

                    else if (setQuantity <= 12)
                    {

                        do
                        {
                            pizza.ChoosePizzageddonPizza(users, location, setQuantity);
                            count++;

                        } while (count <= setQuantity);
                        break;
                    }

                    else
                    {
                        Console.WriteLine("You can't order more than 12 pizzas, Try again next time.");
                        Console.ReadLine();
                        goto Choose;

                    }

                case "2":
                    MenuLocation(users, location);
                    break;

                case "3":
                    AdminMenu(users, location);


                    break;

                case "4":
                    Console.WriteLine("Thanks for using Pizza App!");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Sorry, invalid selection");
                    Console.ReadLine();
                    KevinPizzaMenu(users, location);
                    break;
            }
        }


        public void AdminMenu(List<User> users, int location)
        {
            Choose:
            Console.Clear();
            Console.WriteLine("-----------------------------\n\n" +
                "*****Admin Menu*****\n\n" +
                "-----------------------------\n" +
                "1) Search Users by name\n" +
                "2) Exit.\n" +
                "Please choose one of the options above: ");
            string choice = Console.ReadLine();
            

            switch (choice)
            {
                case "1":

                    Console.WriteLine("Enter your Name:");
                    string firstName = Console.ReadLine();

                    var findUser = new UserRepository(new PizzaPlaceDBContext());
                    

                    string user =  findUser.GetUser(firstName);

                    Console.WriteLine(user);

                    Console.ReadLine();
                    goto Choose;
                    
                case "2":
                    Console.WriteLine("Thanks for using Pizza App!");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;


                default:
                    Console.WriteLine("Sorry, invalid selection");
                    Console.ReadLine();
                    AdminMenu(users, location);
                    break;
            }
        }

        //-----------------------------------------------------------------

        //Pizzageddon Location Menu
        public void PizzageddonMenu(List<User> users, int location)
        {
            string quantity;
            int setQuantity;
            Console.Clear();
            Choose:
            Console.WriteLine("-----------------------------\n\n" +
                "*****Welcome to Pizzageddon*****\n\n" +
                "-----------------------------\n" +
                "1) Order Pizza!\n" +
                "2) Change Location.\n" +
                "3) Exit Application.\n" +
                "Please choose one of the options above: ");
            string choice = Console.ReadLine();
            int count = 1;

            switch (choice)
            {
                case "1":
                    Console.WriteLine("How Many Pizza you want to order?");
                    quantity = Console.ReadLine();
                    setQuantity = Int32.Parse(quantity);

                    

                    if (setQuantity <= 12)
                    {

                        do
                        {
                            pizza.ChoosePizzageddonPizza(users, location, setQuantity);
                            count++;

                        } while (count <= setQuantity);


                        
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You can't order more than 12 pizzas, Try again next time.");
                        Console.ReadLine();
                        goto Choose;
                        
                    }

                    
                    


                case "2":
                    MenuLocation(users, location);
                    break;

                case "3":
                    Console.WriteLine("Thanks for using Pizza App!");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;


                default:
                    Console.WriteLine("Sorry, invalid selection");
                    Console.ReadLine();
                    PizzageddonMenu(users, location);
                    break;
            }
        }
    }
    
}
