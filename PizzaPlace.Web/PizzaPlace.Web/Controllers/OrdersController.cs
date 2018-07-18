using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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



        public ActionResult OrderSorting(string sortOrder, string searchString)
        {

            ViewBag.Message = "List of All Orders:";
            DetailModel ODSP = new DetailModel
            {
                Orders = Repo.GetOrders(),
                Pizza = Repo.GetPizzas(),
                OrderPizza = Repo.GetOrdersPizzas(),
                User = Repo.GetUsers(),
            };

            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            ODSP.Orders = from s in _context.Orders
                       select s;

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    ODSP.Orders = ODSP.Orders.Where(s => s.CustomerName.ToUpper().Contains(searchString.ToUpper())
            //                                || s.CustomerPhoneNumber.Contains(searchString));
            //}

            switch (sortOrder)
            {
                case "Price":
                    ODSP.Pizza = ODSP.Pizza.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    ODSP.Pizza = ODSP.Pizza.OrderByDescending(s => s.Price);
                    break;
                case "Date":
                    ODSP.Orders = ODSP.Orders.OrderBy(s => s.OrderTime);
                    break;
                case "Date_desc":
                    ODSP.Orders = ODSP.Orders.OrderByDescending(s => s.OrderTime);
                    break;
                default:
                    ODSP.Pizza = ODSP.Pizza.OrderBy(s => s.Price);
                    break;
            }

            return View(ODSP);
        }









        //Get: placeAnOrder
        public ActionResult PlaceAnOrder(Users user)
        {
           
            return View();
        }
        // Post: PlaceAnOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceAnOrder(IFormCollection viewCol, DetailModel aModel, Users user)
        {

            int UseridTD = int.Parse(TempData.Peek("userid").ToString());
            int LocationTD = int.Parse(TempData.Peek("locationid").ToString());
            string NameTD = TempData.Peek("firstname").ToString();
            string LastnameTD = TempData.Peek("lastname").ToString();
            string PhoneTD = TempData.Peek("phone").ToString();


            // If the user wants to order more than 12 pizza's 
            if (Count == 12)
            {
                ViewData["mesag"] = "You exceded order limit, you can only order 12 pizzas max. Place your order and try again.";
                return RedirectToAction(nameof(PlaceAnOrder));
            }

            string SelectTopping = viewCol["SelectTopping"];
            string Size = viewCol["SelectSize"];


            int Price = 0;





            // Calculates the total
            if (Size == "Small")
            {
                Price = 5;

                Total = int.Parse(TempData.Peek("order_total").ToString());
                Total += 5;
                TempData["order_total"] = Total;
            }
            else if (Size == "Medium")
            {
                Price = 10;

                Total = int.Parse(TempData.Peek("order_total").ToString());
                Total += 10;
                TempData["order_total"] = Total;
            }
            else if (Size == "Large")
            {
                Price = 16;
                Total = int.Parse(TempData.Peek("order_total").ToString());
                Total += 16;
                TempData["order_total"] = Total;
            }
            

            // Checks if there are enough resourses
            bool checkInventory = Repo.SubsInventory(SelectTopping, UseridTD, Size);

            if (checkInventory == true)
            {

                // Add new pizza
                Repo.AddPizzas(SelectTopping, Size, Price);

                //Check if the counter is at beginning to set it to 1
                if (TempData.Peek("Count").ToString() == "1")
                {
                    Count = 1;

                }
                else
                {
                    Count = int.Parse(TempData.Peek("Count").ToString());
                }

                //For creating just an order for every pizza
                if (Count < 2)
                {
                    // Add New Order
                    Repo.AddOrders(Total, LocationTD, UseridTD);

                }
                Count++;// Increment counter to not create another order.
                TempData["Count"] = Count;



                // Search for the pizza and order Id
                int? orderId = Repo.GetOrderByUserId(UseridTD);
                int? pizzaId = Repo.GetPizzaIdBySize(SelectTopping, Size);
                TempData["orderid"] = orderId;



                // Add new orderPizza
                Repo.AddOrderPizza(orderId, pizzaId);

                return RedirectToAction("PlaceAnOrder");
            }
            // if there are no more toppings for the pizza's in inventory
            else
            {
                ViewData["message"] = "Not Enough resourses to create a pizza, please try again later.";
                return RedirectToAction("PlaceAnOrder");
            }
        }
    



        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var pizzaPlaceDBContext = _context.Orders.Include(o => o.Location).Include(o => o.Users);
            return View(await pizzaPlaceDBContext.ToListAsync());
        }


        
        //public ActionResult ShowOrder()
        //{
        //    return View();
        //}




        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            int orderid = int.Parse(TempData.Peek("orderid").ToString());

           

            var orders = await _context.Orders
                .Include(o => o.Location)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.OrderId == orderid);
            

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

