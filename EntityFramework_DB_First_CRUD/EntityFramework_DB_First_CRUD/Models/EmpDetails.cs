﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFramework_DB_First_CRUD.Models
{
    public class EmpDetails : Employee
    {
        public List<Employee> Employees { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Address1 { get; set; }
        public string Key { get; set; }
    }
}