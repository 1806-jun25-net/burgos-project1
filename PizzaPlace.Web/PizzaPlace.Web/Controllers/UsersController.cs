using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PizzaPlace.DataAccess;
using PizzaPlace.Library;

namespace PizzaPlace.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly PizzaPlaceDBContext _context;
        public PizzaPlaceRepository Repo { get; }

        public UsersController(PizzaPlaceDBContext context, PizzaPlaceRepository repo)
        {
            _context = context;
            Repo = repo;
        }


        public IActionResult Home()
        {
            
            return View();
        }

        public IActionResult AddNewUser()
        {
            Users user = new Users();
            return View(user);
        }

        public IActionResult SearchUser()
        {
            Users user = new Users();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchUser(Users user)
        {
            ////////////////////////////////////////////////////////////////////////////////////////
            //var builder = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //IConfigurationRoot configuration = builder.Build();
            //Console.WriteLine(configuration.GetConnectionString("PizzaPalacedb"));
            //var optionsBuilder = new DbContextOptionsBuilder<PizzaPlaceDBContext>();
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaPalacedb"));
            ////////////////////////////////////////////////////////////////////////////////////////

            //var repo = new UserRepository(new PizzaPalacedbContext(optionsBuilder.Options));
            var users = Repo.GetUsers();
            var aUser = users.FirstOrDefault(x => x.FirstName == user.FirstName);
            if (aUser == null)
            {
                TempData["Error"] = "Error: User not found";
            }
            else
            {
                TempData["userId"] = aUser.UsersId;
                TempData["name"] = aUser.FirstName;
                TempData["last"] = aUser.LastName;
                TempData["phone"] = aUser.Phone;
                if (aUser.LocationId == 1)
                {
                    TempData["loc"] = "Kevin's Pizza";
                }
                else if (aUser.LocationId == 2)
                {
                    TempData["loc"] = "Pizzageddon";
                }
                
            }
            return View();

        }



        public IActionResult UserOrder()
        {
            Users user = new Users();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserOrder(Users user)
        {
            ////////////////////////////////////////////////////////////////////////////////////////
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            Console.WriteLine(configuration.GetConnectionString("PizzaPalacedb"));
            var optionsBuilder = new DbContextOptionsBuilder<PizzaPlaceDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaPalacedb"));
            ////////////////////////////////////////////////////////////////////////////////////////


            var repo = new PizzaPlaceRepository(new PizzaPlaceDBContext(optionsBuilder.Options));
            var users = Repo.GetUsers(); // Get all user
            var userorder = users.FirstOrDefault(g => g.FirstName == user.FirstName); // searching user
            var userID = user.UsersId; // user ID
            var won = Repo.GetOrders(); // Get all Order
            var order = won.Where(q => q.UsersId == userID); // All user order
            var orderPizza = Repo.GetOrders(); // Get all Pizza
            var PizzasUser = orderPizza.Where(q => q.UsersId == userID); // pizza of order

            foreach (var item in order)
            {
                TempData["Order ID:"] = item.OrderId;
                TempData["Order ID:"] = item.OrderTime;

                foreach (var item2 in orderPizza)
                {


                }
            }
            return View();
        }


           

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewUser(UsersModel user)
        {

            bool foundUser = false;
            TempData["firstname"] = user.FirstName;
            TempData["lastname"] = user.LastName;
            TempData["phone"] = user.Phone;
            TempData["Count"] = 1; // Counter for the Pizzas being made
            TempData["order_total"] = 0;


            var allMyUsers = Repo.GetUsers();

            foreach (var aUser in allMyUsers)
            {
                if (aUser.FirstName == user.FirstName && aUser.Phone == user.Phone)
                {
                    user.UsersId = aUser.UsersId;
                    TempData["userid"] = user.UsersId;
                    foundUser = true;
                    break;
                }

            }


            if (foundUser == true)
            {
                TempData["welcome"] = "Welcome Back " + user.FirstName;

            }
            else if (foundUser == false)
            {
                TempData["welcome"] = "Welcome " + user.FirstName;

                //create new user
                Repo.AddUsers(user.FirstName, user.LastName, user.Phone);
                Repo.SaveChanges();
                user.UsersId = Repo.GetUserIDByPhone(user.FirstName, user.Phone);

            }

            return RedirectToAction("ChooseALocation", "Locations", user);
        }

        public IActionResult Login()
        {
            return View();
        }
        





        // GET: Users
        public async Task<IActionResult> Index()
        {
            var pizzaPlaceDBContext = _context.Users.Include(u => u.Location);
            return View(await pizzaPlaceDBContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.Location)
                .FirstOrDefaultAsync(m => m.UsersId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsersId,FirstName,LastName,Phone,LocationId")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", users.LocationId);
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", users.LocationId);
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsersId,FirstName,LastName,Phone,LocationId")] Users users)
        {
            if (id != users.UsersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UsersId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", users.LocationId);
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.Location)
                .FirstOrDefaultAsync(m => m.UsersId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UsersId == id);
        }
    }
}
