using Microsoft.EntityFrameworkCore;
using PizzaPlace.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlace.Library.Repositories
{
    public class PizzaRepository
    {
        private readonly PizzaPlaceDBContext _db;


        public PizzaRepository(PizzaPlaceDBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


        }

        public IEnumerable<Pizza> GetPizza()
        {
            // we don't need to track changes to these, so
            // skip the overhead of doing so
            List<Pizza> pizzas = _db.Pizza.AsNoTracking().ToList();
            return pizzas;
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
        }

        public void Edit(Pizza pizza)
        {
            //uodates the current pizza 
            _db.Update(pizza);

        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
