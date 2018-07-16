using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaPlace.DataAccess;

namespace PizzaPlace.Web.Controllers
{
    public class OrderPizzasController : Controller
    {
        private readonly PizzaPlaceDBContext _context;

        public OrderPizzasController(PizzaPlaceDBContext context)
        {
            _context = context;
        }

        // GET: OrderPizzas
        public async Task<IActionResult> Index()
        {
            var pizzaPlaceDBContext = _context.OrderPizza.Include(o => o.Order).Include(o => o.Pizza);
            return View(await pizzaPlaceDBContext.ToListAsync());
        }

        // GET: OrderPizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderPizza = await _context.OrderPizza
                .Include(o => o.Order)
                .Include(o => o.Pizza)
                .FirstOrDefaultAsync(m => m.OrderPizzaId == id);
            if (orderPizza == null)
            {
                return NotFound();
            }

            return View(orderPizza);
        }

        // GET: OrderPizzas/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "PizzaId", "Name");
            return View();
        }

        // POST: OrderPizzas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderPizzaId,OrderId,PizzaId,Quantity")] OrderPizza orderPizza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderPizza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderPizza.OrderId);
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "PizzaId", "Name", orderPizza.PizzaId);
            return View(orderPizza);
        }

        // GET: OrderPizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderPizza = await _context.OrderPizza.FindAsync(id);
            if (orderPizza == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderPizza.OrderId);
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "PizzaId", "Name", orderPizza.PizzaId);
            return View(orderPizza);
        }

        // POST: OrderPizzas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderPizzaId,OrderId,PizzaId,Quantity")] OrderPizza orderPizza)
        {
            if (id != orderPizza.OrderPizzaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderPizza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderPizzaExists(orderPizza.OrderPizzaId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderPizza.OrderId);
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "PizzaId", "Name", orderPizza.PizzaId);
            return View(orderPizza);
        }

        // GET: OrderPizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderPizza = await _context.OrderPizza
                .Include(o => o.Order)
                .Include(o => o.Pizza)
                .FirstOrDefaultAsync(m => m.OrderPizzaId == id);
            if (orderPizza == null)
            {
                return NotFound();
            }

            return View(orderPizza);
        }

        // POST: OrderPizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderPizza = await _context.OrderPizza.FindAsync(id);
            _context.OrderPizza.Remove(orderPizza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderPizzaExists(int id)
        {
            return _context.OrderPizza.Any(e => e.OrderPizzaId == id);
        }
    }
}
