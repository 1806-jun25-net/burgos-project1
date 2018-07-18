using PizzaPlace.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaPlace.Library
{
    public class PizzaPlaceRepository
    {

        public int userOrder2;
        private readonly PizzaPlaceDBContext _db;


        public PizzaPlaceRepository(PizzaPlaceDBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


        }

        

        public IEnumerable<Users> GetUsers()
        {
            List<Users> users = _db.Users.ToList();
            return users;
        }

        public IEnumerable<Orders> GetOrders()
        {
            List<Orders> order = _db.Orders.ToList();
            return order;
        }

        public IEnumerable<Pizza> GetPizzas()
        {
            List<Pizza> pizzas = _db.Pizza.ToList();
            return pizzas;
        }

        public IEnumerable<OrderPizza> GetOrdersPizzas()
        {
            List<OrderPizza> orderPizza = _db.OrderPizza.ToList();
            return orderPizza;
        }


        


        //////////////////// Get a Sugested Order Pizza //////////////////////////////////////
        public string SugestedOrder(string FindUser, string lastName)
        {
            // Get the user
            var user = _db.Users.FirstOrDefault(g => g.FirstName == FindUser && g.LastName == lastName);
            var userID = user.UsersId;

            // Get the orders
            var allOrders = GetOrders();
            var order = allOrders.LastOrDefault(x => x.UsersId == userID); // Filters all orders table with the user's id

            // Get the order's pizzas
            var allOrdersPizzas = GetOrdersPizzas();
            var orderPerPizza = allOrdersPizzas.LastOrDefault(y => y.OrderId == order.OrderId); // Filters all ordersPizza table with the Order's id


            // Get the pizzas
            var allpizzas = GetPizzas();
            var pizzas = allpizzas.LastOrDefault(y => y.PizzaId == orderPerPizza.PizzaId);// Filters all Pizzas table with the OrdersPizza id


            Console.WriteLine($"\n Order Number: {order.OrderId}");
            Console.WriteLine($"\n Last ordered Pizza: {pizzas.Name}\n" +
                $" Size: {pizzas.Size}\n" +
                $" Price: ${pizzas.Price}\n");
            Console.ReadLine();
            return $"\nLast ordered Pizza: {pizzas.Name}" +
                $"\n Size: {pizzas.Size}" +
                $"\n Price: ${pizzas.Price}";
        }



        ///////////////////// Get's the user's id by its phone number ////////////////////////////////////////////////////
        public int GetUserIDByPhone(string findUser, string phone)
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


        //////////////////////////// Displays the history of the order ////////////////////////////////////////////////////////
        public void DisplayHistoryOfOrder(int? findUser, string phone, int? userId, int location, int? orderId, int? pizzaId)
        {
            //Get user info
            var user = _db.Users.LastOrDefault(g => g.UsersId == findUser);

            //Get location info
            var loc = _db.Locations.LastOrDefault(g => g.LocationId == location);

            //Ger order info
            var order = _db.Orders.LastOrDefault(g => g.OrderId == orderId);

            //Get the order of a pizza
            var orderPizza = _db.OrderPizza.LastOrDefault(g => g.OrderId == order.OrderId);

            //Get Pizza info
            var pizza = _db.Pizza.LastOrDefault(g => g.PizzaId == orderPizza.PizzaId);

            if (user == null)
            {
                Console.WriteLine("No existent user.");
            }

            else
            {
                Console.WriteLine($"First Name: {user.FirstName}\n" +
                    $"Last Name: {user.LastName}\n" +
                    $"Phone: {user.Phone}\n" +
                   $"Location: {loc.Name}\n" +
                   $"Pizza: {pizza.Name}\n" +
                   $"Size: {pizza.Size}\n" +
                   $"Price: ${pizza.Price}.00\n" +
                   $"OrderTime: {order.OrderTime}\n");

            }


        }


        //////////////////////////// Get the user's orders ////////////////////////////////////////////////////////////
        public void GetUserOrders(string firstName, string lastName)
        {

            var user = _db.Users.FirstOrDefault(g => g.FirstName == firstName && g.LastName == lastName);
            var userID = user.UsersId;
            var orders = GetOrders();
            var userOrder = orders.Where(t => t.UsersId == userID);


            var allOrderPizza = GetOrdersPizzas();
            var allPizzas = GetPizzas();


            foreach (var item in userOrder)
            {
                var orderPizza = allOrderPizza.Where(g => g.OrderId == item.OrderId);
                Console.WriteLine("Order No.: " + item.OrderId + "  Date & Time:" + item.OrderTime);

                foreach (var myOrder in orderPizza)
                {

                    var myPizzas = _db.Pizza.Where(g => g.PizzaId == myOrder.PizzaId);

                    foreach (var pizzas in myPizzas)
                    {

                        var pizzaName = _db.Pizza.FirstOrDefault(g => g.PizzaId == pizzas.PizzaId);
                        Console.WriteLine($"First Name: {user.FirstName}\n" +
                        $"Last Name: {user.LastName}\n" +
                        $"Phone: {user.Phone}\n" +
                        $"Pizza: {pizzas.Name}\n" +
                        $"Size: {pizzas.Size}\n" +
                        $"Price: ${pizzas.Price}.00\n");



                    }
                }
                Console.ReadLine();



            }



        }

        /////////////////////////////// Get orders by location ////////////////////////////////////////////////
        public IEnumerable<Orders> GetLocationOrders(int location_id)
        {

            var OrderLocations = _db.Orders.Where(g => g.LocationId == location_id);
            return OrderLocations;
        }

        /////////////////////////////////// Cheaper ////////////////////////////////////////////////////////
        public IEnumerable<Orders> GetLocationOrdersByCheapest(int location_id)
        {

            var OrderLocations = _db.Orders.Where(g => g.LocationId == location_id).OrderBy(g => g.Total);
            return OrderLocations;
        }

        /////////////////////////////// Most expensive ////////////////////////////////////////////////////
        public IEnumerable<Orders> GetLocationOrdersMostExpensive(int location_id)
        {

            var OrderLocations = _db.Orders.Where(g => g.LocationId == location_id).OrderByDescending(g => g.Total);
            return OrderLocations;
        }


        ////////////////////////////// Earliest order in location ///////////////////////////////////////
        public IEnumerable<Orders> GetLocationOrderEarliest(int location_id)
        {

            var OrderLocations = _db.Orders.Where(g => g.LocationId == location_id).OrderBy(g => g.OrderTime);
            return OrderLocations;
        }

        ////////////////////////////// Latest order in location //////////////////////////////////////
        public IEnumerable<Orders> GetLocationOrderLatest(int location_id)
        {

            var OrderLocations = _db.Orders.Where(g => g.LocationId == location_id).OrderByDescending(g => g.OrderTime);
            return OrderLocations;
        }

        ///////////////////////// Substract from inventory //////////////////////////////
        public bool SubsInventory(string Topping, int location, string size)
        {

            var InventoryLoc = _db.Inventory.FirstOrDefault(g => g.LocationId == location);
            if (InventoryLoc == null)
            {
                return false;
            }

            else if (InventoryLoc.Dough <= 0)
            {

                return false;
            }


            else
            {
                //foreach (string toppings in Topping)
                //{


                    switch (Topping)
                    {
                        case "Dough":
                            if (size == "Small" && InventoryLoc.Dough >= 1 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Dough = InventoryLoc.Dough - 1;
                                return true;
                            }
                            else if (size == "Medium" && InventoryLoc.Dough >= 2 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Dough = InventoryLoc.Dough - 2;
                                return true;
                            }
                            else if (size == "Large" && InventoryLoc.Dough >= 3 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Dough = InventoryLoc.Dough - 3;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                            

                        case "Cheese":
                            if (size == "Small" && InventoryLoc.Cheese >= 1 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Cheese = InventoryLoc.Cheese - 1;
                                return true;
                            }
                            else if (size == "Medium" && InventoryLoc.Cheese >= 2 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Cheese = InventoryLoc.Cheese - 2;
                                return true;
                            }
                            else if (size == "Large" && InventoryLoc.Cheese >= 3 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Cheese = InventoryLoc.Cheese - 3;
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                          


                        case "Sausage":

                            if (size == "Small" && InventoryLoc.Sausage >= 1 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Sausage = InventoryLoc.Sausage - 1;
                                return true;
                            }
                            else if (size == "Medium" && InventoryLoc.Sausage >= 2 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Sausage = InventoryLoc.Sausage - 2;
                                return true;
                            }
                            else if (size == "Large" && InventoryLoc.Sausage >= 3 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Sausage = InventoryLoc.Sausage - 3;
                                return true;
                            }
                            else
                            {
                                return true;
                            }

                          

                        case "Pepperoni":

                            if (size == "Small" && InventoryLoc.Pepperoni >= 1 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Pepperoni = InventoryLoc.Pepperoni - 1;
                                return true;
                            }
                            else if (size == "Medium" && InventoryLoc.Pepperoni >= 2 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Pepperoni = InventoryLoc.Pepperoni - 2;
                                return true;
                            }
                            else if (size == "Large" && InventoryLoc.Pepperoni >= 3 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Pepperoni = InventoryLoc.Pepperoni - 3;
                                return true;
                            }
                            else
                            {
                                return true;
                            }
                      

                        case "Bacon":
                            if (size == "Small" && InventoryLoc.Bacon >= 1 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Bacon = InventoryLoc.Bacon - 1;
                                return true;
                            }
                            else if (size == "Medium" && InventoryLoc.Bacon >= 2 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Bacon = InventoryLoc.Bacon - 2;
                                return true;
                            }
                            else if (size == "Large" && InventoryLoc.Bacon >= 3 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Bacon = InventoryLoc.Bacon - 3;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        

                        case "Onions":

                            if (size == "Small" && InventoryLoc.Onions >= 1 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Onions = InventoryLoc.Onions - 1;
                                return true;
                            }
                            else if (size == "Medium" && InventoryLoc.Onions >= 2 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Onions = InventoryLoc.Onions - 2;
                                return true;
                            }
                            else if (size == "Large" && InventoryLoc.Onions >= 3 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Onions = InventoryLoc.Onions - 3;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                   

                        case "Chicken":

                            if (size == "Small" && InventoryLoc.Chicken >= 1 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Chicken = InventoryLoc.Chicken - 1;
                                return true;
                            }
                            else if (size == "Medium" && InventoryLoc.Chicken >= 2 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Chicken = InventoryLoc.Chicken - 2;
                                return true;
                            }
                            else if (size == "Large" && InventoryLoc.Chicken >= 3 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Chicken = InventoryLoc.Chicken - 3;
                                return true;
                            }
                            else
                            {
                                return true;
                            }
                       

                        case "Chorizo":

                            if (size == "Small" && InventoryLoc.Chorizo >= 1 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Chorizo = InventoryLoc.Chorizo - 1;
                                return true;
                            }
                            else if (size == "Medium" && InventoryLoc.Chorizo >= 2 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Chorizo = InventoryLoc.Chorizo - 2;
                                return true;
                            }
                            else if (size == "Large" && InventoryLoc.Chorizo >= 3 && InventoryLoc.LocationId == location)
                            {
                                InventoryLoc.Chorizo = InventoryLoc.Chorizo - 3;
                                return true;
                            }
                            else
                            {
                                return true;
                            }
                       
                    }
                SaveChanges();
                return true;
                //}

              

            }


        }

        //////////////////// Gets the current order's id ////////////////////////////
        public int? GetOrderId(int userId)
        {

            var user = _db.Orders.LastOrDefault(g => g.UsersId == userId);
            if (user == null)
            {
                return 0;
            }

            else
            {
                return user.OrderId;
            }


        }

        public void Edit(Orders order)
        {
            //uodates the current order 
            _db.Update(order);
            SaveChanges();

        }

        ////////////////////////// Sets the total for order ////////////////////////////
        public double? SetTotal(int? findUserId, double? total, double? price, int? orderId)
        {

            var order = _db.Orders.LastOrDefault(g => g.UsersId == findUserId);
            var pizza = _db.OrderPizza.LastOrDefault(x => x.OrderId == orderId);
            if (order == null)
            {
                return 0;
            }

            else if (order.OrderId == pizza.OrderId)
            {
                order.Total += price;
                if (order.Total > 500)
                {
                    Console.WriteLine("You exceeded the total amount!!\n" +
                        "Ordered Canceled! Try again next time.");
                    Console.WriteLine("Thanks for using Pizza App!");
                    Console.ReadLine();
                    Environment.Exit(0);
                    return 0;
                }
                else
                {
                    return order.Total;
                }

            }
            else
            {
                return 0;
            }



        }

        //////////////////// Gets the pizza by size ////////////////////////////
        public int? GetPizzaIdBySize(string pizza, string size)
        {

            var pizzaId = _db.Pizza.LastOrDefault(g => g.Name == pizza && g.Size == size);
            if (pizza == null)
            {
                return 0;
            }

            else
            {
                return pizzaId.PizzaId;
            }


        }

        //////////////////// Gets the Orders by UserId ////////////////////////////
        public int? GetOrderByUserId(int? findUserId)
        {

            var order = _db.Orders.LastOrDefault(g => g.UsersId == findUserId);
            if (order == null)
            {
                return 0;
            }

            else
            {
                return order.OrderId;
            }


        }
        


        //////////////////// Gets the users ////////////////////////////
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
                string Name = user.FirstName + " " + user.LastName;
                return Name;
            }
        }

        //////////////////// Gets the Users by the phone ////////////////////////////
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


        public void AddOrderPizza(int? order, int? pizza)
        {


            var orderPizza = new OrderPizza
            {
                OrderId = order,
                PizzaId = pizza,
                Quantity = 1
            };
            _db.Add(orderPizza);
            SaveChanges();
        }

        public void AddOrders(double? total, int? location, int user)
        {
            var orderTime = DateTime.Now;

            var orders = new Orders
            {
                Total = total,
                LocationId = location,
                UsersId = user,
                OrderTime = orderTime
            };
            _db.Add(orders);
            SaveChanges();
        }

        public void AddPizzas(string name, string size, int price)
        {


            var pizzas = new Pizza
            {
                Name = name,
                Size = size,
                Price = price
            };
            _db.Add(pizzas);
            SaveChanges();
        }

        public void AddLocations(string name)
        {


            var locations = new Locations
            {
                Name = name,
            };
            _db.Add(locations);
            SaveChanges();
        }

        public void AddUsers(string firstName, string lastName, string phone)
        {


            var users = new Users
            {
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
                LocationId = 1
            };
            _db.Add(users);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
