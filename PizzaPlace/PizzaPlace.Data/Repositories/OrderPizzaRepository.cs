using Microsoft.EntityFrameworkCore;
using PizzaPlace.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlace.Library.Repositories
{
    public class OrderPizzaRepository
    {
        private readonly PizzaPlaceDBContext _db;


        public OrderPizzaRepository(PizzaPlaceDBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


        }

        public IEnumerable<OrderPizza> GetOrderPizza()
        {
            // we don't need to track changes to these, so
            // skip the overhead of doing so
            List<OrderPizza> orderPizzas = _db.OrderPizza.AsNoTracking().ToList();
            return orderPizzas;
        }


        public void AddOrderPizza(int order, int? pizza, int quantity)
        {


            var orderPizza = new OrderPizza
            {
                OrderId = order,
                PizzaId = pizza,
                Quantity = quantity
            };
            _db.Add(orderPizza);
        }

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

        public double? SetTotal(int? findUserId, double? total, double? price, int? orderId)
        {

            var order = _db.Orders.LastOrDefault(g => g.UsersId == findUserId);
            var pizza = _db.OrderPizza.LastOrDefault(x => x.OrderId == orderId);
            if (order == null)
            {
                return 0;
            }

            else if(order.OrderId == pizza.OrderId)
            {
                order.Total += price;
                if(order.Total > 500)
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

        public void Edit(OrderPizza orderPizza)
        {
            //updates the current orderPizza 
            _db.Update(orderPizza);

        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
