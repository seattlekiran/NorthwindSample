using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace ConsoleApplication1.Controllers
{
    public class AlphabeticalListOfProductsController : EntitySetController<Alphabetical_list_of_product, int>
    {
        NorthwindEntities db = new NorthwindEntities();

        protected override Alphabetical_list_of_product GetEntityById(int id)
        {
            return db.Alphabetical_list_of_products.Where(p => p.ProductID == id).Single();
        }

        public override IQueryable<Alphabetical_list_of_product> Get()
        {
            return db.Alphabetical_list_of_products;
        }
    }
}
