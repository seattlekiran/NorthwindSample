using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Controllers
{
    public class SuppliersController : EntitySetController<Supplier, int>
    {
        NorthwindEntities db = new NorthwindEntities();

        public override IQueryable<Supplier> Get()
        {
            return db.Suppliers;
        }

        protected override Supplier GetEntityById(int id)
        {
            return db.Suppliers.Where(splr => splr.SupplierID == id).Single();
        }
    }
}
