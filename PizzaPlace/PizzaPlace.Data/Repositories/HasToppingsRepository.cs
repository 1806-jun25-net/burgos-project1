using Microsoft.EntityFrameworkCore;
using PizzaPlace.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlace.Library.Repositories
{
    public class HasToppingsRepository
    {
        private readonly PizzaPlaceDBContext _db;


        public HasToppingsRepository(PizzaPlaceDBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


        }

        //public IEnumerable<HasToppings> GetHasToppings()
        //{
        //    // we don't need to track changes to these, so
        //    // skip the overhead of doing so
        //    List<HasToppings> hasToppings = _db.HasToppings.AsNoTracking().ToList();
        //    return hasToppings;
        //}


        public void AddHasTopping(int? pizzaId, int locationId)
        {


            var hasTopping = new HasToppings
            {
                PizzaId = pizzaId,
                LocationId = locationId
            };
            _db.Add(hasTopping);
            SaveChanges();
        }

        public void Edit(HasToppings hasTopping)
        {
            //updates the current orderPizza 
            _db.Update(hasTopping);

        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
