using PizzaPlace.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaPlace.Library.Models
{
    public class Pizza : IPizza
    {
        //Variable declaration
        public string Name { get; set ; }
        public string Size { get; set; }
        public float Price { get; set; }
        double IPizza.Price { get; set; }

        //Variable declaration
        string Ham { get; set; }
        string Sausage { get; set; }
        string Pepperoni { get; set; }
        string Bacon { get; set; }
        string Onions { get; set; }
        string Chicken { get; set; }
        string Chorizo { get; set; }

        double total = 0;


        //////////////////////////////////////////////////////////////////////

        List<string> Topping = new List<string>();

        //----------------------------------------------------------------------
        //Objects
        
        Order SubmitOrder = new Order();

        //Methods:
        //----------------------------------------------------------------------


        private void CreatePizza(string pizza, List<User> users, int location, int setQuantity)
        {
            PizzaSize(pizza, users, location, setQuantity);
        }


        
        //--------------------------------------------------------------------

        //Method to get a pizza from KevinPizza
        public void ChooseKevinPizza(List<User> users, int location, int setQuantity)
        {

            Console.Clear();
            Console.WriteLine("--------------------------------\n\n" +
                "*****Welcome to Pizzageddon*****\n\n" +
                "--------------------------------\n" +
                "--------Choose the Pizza--------\n\n" +
                 "1) Cheese Pizza\n" +
                 "2) Pepperoni Pizza\n" +
                 "3) Sausage Pizza\n" +
                 "4) Custom Pizza\n" +
                 "5) Exit.\n" +
                 "Please choose one of the options above: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Topping.Add("Cheese");
                    CreatePizza("Cheese", users, location, setQuantity);
                    break;


                case "2":
                    Topping.Add("Pepperoni");
                    CreatePizza("Pepperoni", users, location, setQuantity);
                    break;

                case "3":
                    Topping.Add("Sausage");
                    CreatePizza("Sausage", users, location, setQuantity);
                    break;

                case "4":
                    CreatePizza("Custom", users, location, setQuantity);
                    break;

                case "5":
                    Console.WriteLine("Thanks for using Pizza App!");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;


                default:
                    Console.WriteLine("Sorry, invalid selection");
                    Console.ReadLine();
                    ChooseKevinPizza(users, location, setQuantity);
                    break;
            }

        }


        
        //------------------------------------------------------------------

        //Method to get a pizza type from Pizzageddon
        public void ChoosePizzageddonPizza(List<User> users, int location, int setQuantity)
        {

            Console.Clear();
            Console.WriteLine("--------------------------------\n\n" +
                "*****Welcome to Pizzageddon*****\n\n" +
                "--------------------------------\n" +
                "--------Choose the Pizza--------\n\n" +
                "1) Cheesy Pizza\n" +
                "2) Peperoni Pizza\n" +
                "3) Sausage Pizza\n" +
                "4) Custom Pizza\n" +
                "5) Exit.\n" +
                "Please choose one of the options above: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":

                    CreatePizza("Cheese", users, location, setQuantity);
                    break;


                case "2":
                    CreatePizza("Pepperoni", users, location, setQuantity);
                    break;

                case "3":
                    CreatePizza("Sausage", users, location, setQuantity);
                    break;

                case "4":
                    CreatePizza("Custom", users, location, setQuantity);
                    break;

                case "5":
                    Console.WriteLine("Thanks for using Pizza App!");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;


                default:
                    Console.WriteLine("Sorry, invalid selection");
                    Console.ReadLine();
                    ChoosePizzageddonPizza(users, location, setQuantity);
                    break;
            }

        }

        
        //---------------------------------------------------------------------

        //Method to get the size of the desired pizza
        public void PizzaSize(string pizza, List<User> users, int location, int setQuantity)
        {
            //List to set all the toppings in a pizza

            List<string> Topping = new List<string>();
            List<int> ToppingId = new List<int>();

            //Sets how many pizza dough is needed to create pizza
            for (int i = 1; i <= setQuantity; i++)
            {
                Topping.Add("Dough");
            }

            Console.Clear();
            Console.WriteLine("--------------------------------\n\n" +
                "*****Welcome to PR Pizza*****\n\n" +
                "--------------------------------\n" +
                "--------Choose the Size of the Pizza--------\n\n" +
                "1) Small Pizza--------------> $5.00\n" +
                "2) Medium Pizza-------------> $10.00\n" +
                "3) Large Pizza--------------> $16.00\n" +
                "4) Exit.\r\n" +
                "Please choose one of the options above: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    if(pizza == "Custom")
                    {
                        total += 5;
                        ToppingsMenu(users, pizza, "Small", 5, location, setQuantity, total);

                    }
                    else
                    {
                        if (location == 1)
                        {

                            total += 5;
                            Topping.Add(pizza);
                            ToppingId.Add(2);
                            SubmitOrder.PrintOrder(users, pizza, "Small", 5, Topping, location, ToppingId, setQuantity, total);
                        }
                        else
                        {
                            total += 5;
                            Topping.Add(pizza);
                            ToppingId.Add(10);
                            SubmitOrder.PrintOrder(users, pizza, "Small", 5, Topping, location, ToppingId, setQuantity, total);
                        }
                        
                        
                    }
                    
                    break;


                case "2":
                    if (pizza == "Custom")
                    {
                        total += 10;
                        ToppingsMenu(users, pizza, "Medium", 10, location, setQuantity, total);
                    }
                    else
                    {
                        
                        if (location == 1)
                        {
                            total += 10;
                            Topping.Add(pizza);
                            ToppingId.Add(3);
                            SubmitOrder.PrintOrder(users, pizza, "Medium", 10, Topping, location, ToppingId, setQuantity, total);
                        }
                        else
                        {
                            total += 10;
                            Topping.Add(pizza);
                            ToppingId.Add(11);
                            SubmitOrder.PrintOrder(users, pizza, "Medium", 10, Topping, location, ToppingId, setQuantity, total);
                        }
                    }
                    
                    break;

                case "3":
                    if (pizza == "Custom")
                    {
                        total += 16;
                        ToppingsMenu(users, pizza, "Large", 16, location, setQuantity, total);
                    }
                    else
                    {
                        if (location == 1)
                        {
                            total += 16;
                            Topping.Add(pizza);
                            ToppingId.Add(4);
                            SubmitOrder.PrintOrder(users, pizza, "Large", 16, Topping, location, ToppingId, setQuantity, total);
                        }
                        else
                        {
                            total += 16;
                            Topping.Add(pizza);
                            ToppingId.Add(12);
                            SubmitOrder.PrintOrder(users, pizza, "Large", 16, Topping, location, ToppingId, setQuantity, total);
                        }
                    }
                    
                    break;
                    
                case "4":
                    Console.WriteLine("Thanks for using Pizza App!");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;


                default:
                    Console.WriteLine("Sorry, invalid selection");
                    Console.ReadLine();
                    PizzaSize(pizza, users, location, setQuantity);
                    break;
            }

        }


        //Menu for toppings for the pizza.
        public void ToppingsMenu(List<User> users, string pizza, string size, int price, int location, int setQuantity, double total)
        {
            //List to set all the toppings in a pizza
            List<int> ToppingId = new List<int>();
            Console.Clear();
            Console.WriteLine("------------------------------------\n\n" +
                "*****Toppings Menu*****\n\n" +
                "------------------------------------\n" +
                "--------Choose Your Toppings--------\n" +
                 "1) Cheese\n" +
                 "2) Sausage\n" +
                 "3) Pepperoni\n" +
                 "4) Bacon\n" +
                 "5) Onions\n" +
                 "6) Chicken\n" +
                 "7) Chorizo\n" +
                 "8) Submit Your Order!\n" +
                 "9) Cancel Order\r\n" +
                 "Please choose one of the options above: ");


            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Cheese was added to the pizza.\r\n");
                    Topping.Add("Cheese");
                    if (location == 1)
                    {
                        ToppingId.Add(2);
                    }
                    else
                    {
                        ToppingId.Add(10);
                    }

                    Console.ReadLine();
                    ToppingsMenu(users, pizza, size, price, location, setQuantity, total);
                    break;


                case "2":
                    Console.WriteLine("Sausage was added to the pizza.\r\n");
                    Topping.Add("Sausage");
                    if (location == 1)
                    {
                        ToppingId.Add(4);
                    }
                    else
                    {
                        ToppingId.Add(12);
                    }

                    Console.ReadLine();
                    ToppingsMenu(users, pizza, size, price, location, setQuantity, total);
                    break;

                case "3":
                    Console.WriteLine("Pepperoni was added to the pizza.\r\n");
                    Topping.Add("Pepperoni");
                    if (location == 1)
                    {
                        ToppingId.Add(3);
                    }
                    else
                    {
                        ToppingId.Add(11);
                    }
                    Console.ReadLine();
                    ToppingsMenu(users, pizza, size, price, location, setQuantity, total);
                    break;

                case "4":
                    Console.WriteLine("Bacon was added to the pizza.\r\n");
                    Topping.Add("Bacon");
                    if (location == 1)
                    {
                        ToppingId.Add(5);
                    }
                    else
                    {
                        ToppingId.Add(13);
                    }
                    Console.ReadLine();
                    ToppingsMenu(users, pizza, size, price, location, setQuantity, total);
                    break;

                case "5":
                    Console.WriteLine("Onions was added to the pizza.\r\n");
                    Topping.Add("Onions");
                    if (location == 1)
                    {
                        ToppingId.Add(6);
                    }
                    else
                    {
                        ToppingId.Add(14);
                    }
                    Console.ReadLine();
                    ToppingsMenu(users, pizza, size, price, location, setQuantity, total);
                    break;

                case "6":
                    Console.WriteLine("Chicken was added to the pizza.\r\n");
                    Topping.Add("Chicken");
                    if (location == 1)
                    {
                        ToppingId.Add(7);
                    }
                    else
                    {
                        ToppingId.Add(15);
                    }
                    Console.ReadLine();
                    ToppingsMenu(users, pizza, size, price, location, setQuantity, total);
                    break;

                case "7":
                    Console.WriteLine("Chorizo was added to the pizza.\r\n");
                    Topping.Add("Chorizo");
                    if (location == 1)
                    {
                        ToppingId.Add(8);
                    }
                    else
                    {
                        ToppingId.Add(16);
                    }
                    Console.ReadLine();
                    ToppingsMenu(users, pizza, size, price, location, setQuantity, total);
                    break;

                case "8":
                    Order Print = new Order();
                    Print.PrintOrder(users, pizza, size, price, Topping, location, ToppingId, setQuantity, total);
                    break;

                case "9":
                    Console.WriteLine("Order Canceled! \r\nThanks for using Pizza App!");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;


                default:
                    Console.WriteLine("Sorry, invalid selection");
                    Console.ReadLine();
                    ToppingsMenu(users, pizza, size, price, location, setQuantity, total);
                    break;
            }
        }

    }
}
