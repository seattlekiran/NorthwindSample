using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Controllers
{
    public class ProductsController : EntitySetController<Product, int>
    {
        NorthwindEntities db = new NorthwindEntities();

        public override IQueryable<Product> Get()
        {
            return db.Products;
        }

        protected override Product GetEntityById(int id)
        {
            return db.Products.Where(prd => prd.ProductID == id).Single();
        }
    }
}
