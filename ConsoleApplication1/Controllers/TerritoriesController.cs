using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Controllers
{
    public class TerritoriesController : EntitySetController<Territory, string>
    {
        NorthwindEntities db = new NorthwindEntities();

        public override IQueryable<Territory> Get()
        {
            return db.Territories;
        }

        protected override Territory GetEntityById(string id)
        {
            return db.Territories.Where(tr => tr.TerritoryID == id).Single();
        }
    }
}
