using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        List<Student> studentlist = new List<Student>();

        public AccountController()
        {
            studentlist.Add(new Student() { StudentId = 1, StudentName = "John", Age = 20, IsActive = true });
            studentlist.Add(new Student() { StudentId = 2, StudentName = "Ram", Age = 21, IsActive = true, Password = "ram123" });
            studentlist.Add(new Student() { StudentId = 3, StudentName = "Harsh", Age = 23, IsActive = true });
            studentlist.Add(new Student() { StudentId = 4, StudentName = "Chetan", Age = 24, IsActive = true });
            studentlist.Add(new Student() { StudentId = 5, StudentName = "Bansi", Age = 25, IsActive = true });
            studentlist.Add(new Student() { StudentId = 6, StudentName = "Sahil", Age = 26, IsActive = true });
            studentlist.Add(new Student() { StudentId = 7, StudentName = "Rutin", Age = 27, IsActive = true });
            studentlist.Add(new Student() { StudentId = 8, StudentName = "Rob", Age = 28, IsActive = true });
            studentlist.Add(new Student() { StudentId = 9, StudentName = "Manjari", Age = 29, IsActive = true });
            studentlist.Add(new Student() { StudentId = 10, StudentName = "Steve", Age = 22, IsActive = false });
        }
        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("CallPartialView", "Account");
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            Student student = studentlist.FirstOrDefault(x => x.StudentId == id);

            return View(student);
        }

        //[ChildActionOnly]
        public ActionResult CallPartialView()
        {
            return PartialView("_Details");
        }
    }
}