using PizzaPlace.Data;
using PizzaPlace.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaPlace.Library.Models
{
    public class Order
    {
        public string OrderNumber { get; set; }
        public DateTime OrderTime { get; private set; }
        int count = 0;
        double? Total = 0;




        //Pizza List
        List<Pizza> listPizza = new List<Pizza>();

        List<Object> ToppingList = new List<Object>();



        //----------------------------------------------------------------------------------------------

        public void PrintOrder(List<User> users,string pizza,string size, int price, List<string>Topping, int location, List<int> ToppingId, int setQuantity, double? total)
        {

            //Pizza Object
            Pizza setPizza = new Pizza();

            OrderTime = DateTime.Now;
            Console.WriteLine("**Your Order**\n\n");
            users.ForEach(item => Console.Write("FirstName: " + item.FirstName + "\n" +
                "LastName: "  + item.LastName + "\n" +
                "Phone Number: " + item.Phone + "\n"));

            Console.WriteLine( 
                $"Pizza: {pizza}\n" +
                $"Size: {size}\n" +
                $"Price: ${price}.00 \n" +
                $"Toppings: ");

            //Topping.ForEach(Console.WriteLine);
            foreach (string toppings in Topping)
            {
                Console.WriteLine("          " + toppings);
            }
            Console.WriteLine($"\nSubmited time: {OrderTime}");
            Console.ReadLine();

            /////////////////////////// Add Pizza to list /////////////////////////////////////
            setPizza.Name = pizza;
            setPizza.Size = size;
            setPizza.Price = price;

            //list for pizzas
            listPizza.Add(setPizza);
            //list for objects 
            ToppingList.Add(ToppingId);
            

            Console.ReadLine();


            ///////////////////////// Substract from inventory ///////////////////////////
            var CheckInventory = new OrdersRepository(new PizzaPlaceDBContext());
            CheckInventory.SubsInventory(Topping, location, size);

            //////////////////////////// Data Insertion ///////////////////////////////////


            // Search the user and extract its id.
            var findUser = new UserRepository(new PizzaPlaceDBContext());
            var findOrderId = new OrderPizzaRepository(new PizzaPlaceDBContext());

            string firstName = " ";
            string phone = " ";

            foreach (User i in users)
            {
                firstName = i.FirstName.ToString();
                phone = i.Phone.ToString();
            }
            
            int? userId = findUser.GetUserIDByPhone(firstName, phone);


            //create new pizza
            CreatePizza(pizza, size, price);


            //create new order
            if(count < 1)
            {
                total = 0;
                CreateOrder(total, location, userId.Value, OrderTime);
            }
            count++;
           
            //find the order id
            int? orderId = findOrderId.GetOrderByUserId(userId);

            //Find the pizza id
            int? pizzaId = findOrderId.GetPizzaIdBySize(pizza, size);

            //Create an order Pizza
            CreateOrderPizza(orderId.Value, pizzaId, setQuantity);

            //int  toppingId = ToppingId[0];

            //Creates toppings for pizza
            CreateHasTopping(pizzaId, location);

            double? priceDouble = double.Parse(price.ToString());
            

            findOrderId.SetTotal(userId, Total, price, orderId);
            findOrderId.SaveChanges();

            var history = new OrdersRepository(new PizzaPlaceDBContext());
            history.DisplayHistoryOfOrder(userId, phone, userId, location, orderId, pizzaId);
          
            Console.WriteLine("Your Order Was Submited sucessfully, Thank You!");
            Console.ReadLine();





        }
        
       ///////////////// Functions to call for data insertion /////////////////////
       

        //Insert statement for user, it saves the current user "logged in"
        public void CreateUser(List<User> users, int location)
        {
            var newUser = new UserRepository(new PizzaPlaceDBContext());

            string firstName = " "; 
            string lastName = " ";
            string phone = " ";

            foreach (User i in users)
            {
                firstName = i.FirstName.ToString();
                lastName = i.LastName.ToString();
                phone = i.Phone.ToString();
            }
            

            newUser.AddUsers(firstName, lastName, phone, location);
            newUser.SaveChanges();
        }

        //////////////////////////////////////////////////////////////////////////
        public void CreateOrder(double? total, int location, int userID, DateTime OrderTime)
        {
            var newOrder = new OrdersRepository(new PizzaPlaceDBContext());


            newOrder.AddOrders(total, location, userID, OrderTime);
            newOrder.SaveChanges();
        }
        
        //////////////////////////////////////////////////////////////////////////
        public void CreatePizza(string name, string size, int price)
        {
            var newPizza = new PizzaRepository(new PizzaPlaceDBContext());

            newPizza.AddPizzas(name, size, price);
            newPizza.SaveChanges();
        }
        //////////////////////////////////////////////////////////////////////////
        public void CreateOrderPizza(int order, int? pizza, int quantity)
        {
            var newOrderPizza = new OrderPizzaRepository(new PizzaPlaceDBContext());

            newOrderPizza.AddOrderPizza(order, pizza, quantity);
            newOrderPizza.SaveChanges();
        }

        //////////////////////////////////////////////////////////////////////////
        public void CreateHasTopping(int? pizza, int location)
        {
            var newHasTopping = new HasToppingsRepository(new PizzaPlaceDBContext());

            newHasTopping.AddHasTopping(pizza, location);
            newHasTopping.SaveChanges();
        }

    }
}
