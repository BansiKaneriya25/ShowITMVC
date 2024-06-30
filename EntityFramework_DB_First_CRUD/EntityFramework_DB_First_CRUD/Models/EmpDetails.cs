using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFramework_DB_First_CRUD.Models
{
    public class EmpDetails : Employee
    {
        public List<Employee> Employees { get; set; }
        public EmployeeIND EmployeesIND { get; set; }
        public EmployeeCND EmployeesCND { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Address1 { get; set; }
        public string Key { get; set; }
    }

    public class EmpDetails1 : Employee
    {
        public Employee Employees { get; set; }
        public EmployeeDetail EmployeeDetails { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Address1 { get; set; }
        public string Key { get; set; }
    }
}