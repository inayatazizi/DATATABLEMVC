using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATATABLEMVC.Models
{
    //public class EmployeeModal
    //{
    //    public List<Employee> Employees { get; set; }
    //    public int CurrentIndex { get; set; }
    //    public int PageCount { get; set; }
    //}
    public class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public string Location { get; set; }
        public int Age { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateString { get; set; }
        public int Salary { get; set; }
    }
}
