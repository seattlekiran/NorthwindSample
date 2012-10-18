using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Controllers
{
    public class CategorySalesFor1997Controller : EntitySetController<Category_Sales_for_1997, string>
    {
        NorthwindEntities db = new NorthwindEntities();

        public override IQueryable<Category_Sales_for_1997> Get()
        {
            return db.Category_Sales_for_1997;    
        }

        protected override Category_Sales_for_1997 GetEntityById(string id)
        {
            return db.Category_Sales_for_1997.Where(c => c.CategoryName == id).Single();
        }
    }
}
