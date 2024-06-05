using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public DateTime DOB { get; set; }
        public decimal Money { get; set; }
    }

    public enum Gender
    {
        Male, Female
    }
}