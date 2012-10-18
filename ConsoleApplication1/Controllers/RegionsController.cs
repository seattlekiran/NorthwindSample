using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Controllers
{
    public class RegionsController : EntitySetController<Region, int>
    {
        NorthwindEntities db = new NorthwindEntities();

        public override IQueryable<Region> Get()
        {
            return db.Regions;
        }

        protected override Region GetEntityById(int id)
        {
            return db.Regions.Where(rgn => rgn.RegionID == id).Single();
        }
    }
}
