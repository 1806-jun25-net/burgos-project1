using Microsoft.EntityFrameworkCore;
using PizzaPlace.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlace.Library.Repositories
{
    public class InventoryRepository
    {
        private readonly PizzaPlaceDBContext _db;


        public InventoryRepository(PizzaPlaceDBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


        }

        public IEnumerable<Inventory> GetInventory()
        {
            // we don't need to track changes to these, so
            // skip the overhead of doing so
            List<Inventory> inventories = _db.Inventory.AsNoTracking().ToList();
            return inventories;
        }


        

        public void Edit(Inventory inventory)
        {
            //uodates the current inventory 
            _db.Update(inventory);

        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
