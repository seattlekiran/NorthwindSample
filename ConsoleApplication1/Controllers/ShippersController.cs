using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Controllers
{
    public class ShippersController : EntitySetController<Shipper, int>
    {
        NorthwindEntities db = new NorthwindEntities();

        public override IQueryable<Shipper> Get()
        {
            return db.Shippers;
        }

        protected override Shipper GetEntityById(int id)
        {
            return db.Shippers.Where(shp => shp.ShipperID == id).Single();
        }
    }
}
