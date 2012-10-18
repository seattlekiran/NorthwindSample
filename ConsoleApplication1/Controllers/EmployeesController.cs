using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Controllers
{
    public class EmployeesController : EntitySetController<Employee, int>
    {
        NorthwindEntities db = new NorthwindEntities();

        public override IQueryable<Employee> Get()
        {
            return db.Employees;
        }

        protected override Employee GetEntityById(int id)
        {
            return db.Employees.Where(emp => emp.EmployeeID == id).Single();
        }

        public Photo GetPhotoInfo(int parentId)
        {
            Employee employee = db.Employees.Where(emp => emp.EmployeeID == parentId).Single();

            Photo ph = new Photo();
            ph.EmployeeID = employee.EmployeeID;
            ph.Employee = employee;
            ph.Bytes = employee.Photo;

            return ph;
        }
    }
}
