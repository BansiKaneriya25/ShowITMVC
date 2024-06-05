using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        List<Student> studentlist = new List<Student>();

        public HomeController()
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            return View(studentlist);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //Binding to primitive type
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Student student = studentlist.FirstOrDefault(x => x.StudentId == id);
            return View(student);
        }

        //Convert query string to action method param
        [HttpGet]
        public ActionResult EditStaticRecord(int id, string studentName)
        {
            return View();
        }

        //Binding to Complex Type
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            //
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}