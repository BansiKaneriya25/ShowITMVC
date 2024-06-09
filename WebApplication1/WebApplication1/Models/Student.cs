using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        public string StudentName { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime DOB { get; set; }
        [Required]
        public decimal Money { get; set; }
        public HttpPostedFileBase File { get; set; }

        [Required]
        public string TestName { get; set; }
    }

    public enum Gender
    {
        Male, Female
    }
}