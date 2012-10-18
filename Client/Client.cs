using Client.ServiceReference1.ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class Client
    {
        static Uri baseUri = new Uri(string.Format("http://{0}:9095/", Environment.MachineName));

        static void Main(string[] args)
        {
            Container cntr = new Container(baseUri);

            foreach (var emp in cntr.Employees)
            {
                cntr.LoadProperty(emp, "PhotoInfo");

                Console.WriteLine(emp.PhotoInfo == null);
            }
        }
    }
}
