using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Controllers
{
    public class PhotosController : EntitySetController<Photo, int>
    {
        NorthwindEntities db = new NorthwindEntities();

        protected override Photo GetEntityById(int id)
        {
            var employee = db.Employees.Where(emp => emp.EmployeeID == id).Single();
            
            Photo ph = new Photo();
            ph.EmployeeID = employee.EmployeeID;
            ph.Bytes = employee.Photo;

            return ph;
        }

        public Employee GetEmployee(int parentId)
        {
            return db.Employees.Where(emp => emp.EmployeeID == parentId).Single();
        }
    }
}
