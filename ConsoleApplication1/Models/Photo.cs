using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Models
{
    public partial class Employee
    {
        public Photo PhotoInfo { get; set; }
    }

    public class Photo
    {
        public int EmployeeID { get; set; }

        public byte[] Bytes { get; set; }

        public Employee Employee { get; set; }
    }
}
