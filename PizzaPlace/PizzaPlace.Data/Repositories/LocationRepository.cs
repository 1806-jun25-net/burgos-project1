using Microsoft.EntityFrameworkCore;
using PizzaPlace.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlace.Library.Repositories
{
    public class LocationRepository
    {
        private readonly PizzaPlaceDBContext _db;


        public LocationRepository(PizzaPlaceDBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


        }

        public IEnumerable<Locations> GetLocation()
        {
            // we don't need to track changes to these, so
            // skip the overhead of doing so
            List<Locations> locations = _db.Locations.AsNoTracking().ToList();
            return locations;
        }


        public void AddLocations(string name)
        {


            var locations = new Locations
            {
                Name = name,
            };
            _db.Add(locations);
        }

        public void Edit(Locations locations)
        {
            //uodates the current location 
            _db.Update(locations);

        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
