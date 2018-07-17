using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaPlace.DataAccess;
using PizzaPlace.Library;
using PizzaPlace.Web.Models;

namespace PizzaPlace.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly PizzaPlaceDBContext _context;
        public PizzaPlaceRepository Repo { get; }
        public int Count = 0;
        public int Total = 0;

        public OrdersController(PizzaPlaceDBContext context, PizzaPlaceRepository repo)
        {
            _context = context;
            Repo = repo;
        }

        //Get: placeAnOrder
        public IActionResult PlaceAnOrder()
        {
            
            //OrdersPizzaModel orderPizza = new OrdersPizzaModel();

            return View();
        }

        // Post: PlaceAnOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceAnOrder( OrdersPizzaModel orderPizza,  Users user)
        {
            
            PizzaModel pizza = new PizzaModel();
            OrdersModel order = new OrdersModel();
            DetailModel myModel = new DetailModel();

            pizza.Name = myModel.SelectTopping.Value;
            pizza.Size = myModel.SelectSize.Value;

            // Sets the values
            order.UsersId = user.UsersId;
            order.LocationId = user.LocationId;
            order.Total = Total;


            if (Count <= 12)
            {


                // Calculates the total
                if (pizza.Size == "small")
                {
                    pizza.Price = 5;
                    Total = Total += 5;
                }
                else if (pizza.Size == "medium")
                {
                    pizza.Price = 10;
                    Total = Total += 10;
                }
                else if (pizza.Size == "large")
                {
                    pizza.Price = 16;
                    Total = Total += 16;
                }

                int price = int.Parse(pizza.Price.ToString());

                // Checks if there are enough resourses
                bool checkInventory = Repo.SubsInventory(pizza.Name, user.UsersId, pizza.Size);

                if (checkInventory == true)
                {

                    // Add new pizza
                    Repo.AddPizzas(pizza.Name, pizza.Size, price);

                    // For creating just an order for every pizza
                    if (Count < 1)
                    {
                        // Add New Order
                        Repo.AddOrders(Total, order.LocationId, user.UsersId);

                    }
                    Count++;



                    // Search for the pizza and order Id
                    int? pizzaId = Repo.GetPizzaIdBySize(pizza.Name, pizza.Size);
                    int? orderId = Repo.GetOrderId(user.UsersId);


                    // Add new orderPizza
                    Repo.AddOrderPizza(orderId, pizzaId);

                    return RedirectToAction("PlaceAnOrder");
                }
                // if there are no more toppings for the pizza's
                else
                {
                    ViewData["message"] = "Not Enough resourses to create a pizza, please try again later.";
                    return RedirectToAction("PlaceAnOrder");
                }
            }
            // If the user wants to order more than 12 pizza's 
            else
            {
                ViewData["message"] = "You exceded order limit, you can only order 12 pizzas max. Place your order and try again.";
                return RedirectToAction("PlaceAnOrder");
            }


        }





        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var pizzaPlaceDBContext = _context.Orders.Include(o => o.Location).Include(o => o.Users);
            return View(await pizzaPlaceDBContext.ToListAsync());
        }

        public ActionResult ShowOrder()
        {
            return View();
        }




        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Location)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name");
            ViewData["UsersId"] = new SelectList(_context.Users, "UsersId", "FirstName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,Total,LocationId,UsersId,OrderTime")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", orders.LocationId);
            ViewData["UsersId"] = new SelectList(_context.Users, "UsersId", "FirstName", orders.UsersId);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", orders.LocationId);
            ViewData["UsersId"] = new SelectList(_context.Users, "UsersId", "FirstName", orders.UsersId);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,Total,LocationId,UsersId,OrderTime")] Orders orders)
        {
            if (id != orders.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", orders.LocationId);
            ViewData["UsersId"] = new SelectList(_context.Users, "UsersId", "FirstName", orders.UsersId);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Location)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
