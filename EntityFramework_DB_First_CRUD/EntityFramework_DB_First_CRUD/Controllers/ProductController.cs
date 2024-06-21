using EntityFramework_DB_First_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFramework_DB_First_CRUD.Controllers
{
    public class ProductController : Controller
    {
        private ShowDotNetITEntities db = new ShowDotNetITEntities();
        public ActionResult Index()
        {


            //IEnumerable 
            var empIEnumerable = db.Employees.Where(x => x.IsActive).AsEnumerable();
            empIEnumerable = empIEnumerable.Take(2);
            //select * from employee where isactive = 1
            //select top 2 * from employee where isactive = 1

            //IQueryable
            var empIQueryable = db.Employees.AsQueryable().Where(x => x.IsActive);
            empIQueryable = empIQueryable.Take(2);
            //select top 2 * from employee where isactive = 1

            //Group By
            var empGroupBy = db.Employees.GroupBy(x => x.Grade).OrderByDescending(x => x.Key)
                .Select(x => new EmpDetails { Key = x.Key, Employees = x.OrderByDescending(z => z.Name).ToList() })
                //.Select(x => new { Key = x.Key, Employees = x.OrderByDescending(z => z.Name).ToList() })
                //.Select(x => new { Employees = x.OrderByDescending(z => z.Name).ToList() })
                .ToList();

            //take only Grade B data
            //empGroupBy.Where(x => x.Key == "B");

            var single = db.Employees.SingleOrDefault(x => x.Name == "ABC");

            //var abc = db.Products.SqlQuery("select * from Products where Price < 15").ToList();

            //Lazy Laoding
            var list = db.Products.ToList(); // 10000 - 20 sec
            //Business
            var abc = list.Where(x => x.Price < 15).ToList(); // min 1 sec // NO DB CONNECTION

            //LINQ Start

            var emp = db.Employees.ToList();

            //Select

            var empName = emp.Where(x => x.Grade == "A").Select(x => x.Name).Distinct().ToList(); //EF

            var linqEmpName = (from emp1 in db.Employees.Where(x => x.Grade == "A").ToList()
                               select emp1.Name).Distinct().ToList(); //LINQ

            var linqEmp = (from emp1 in db.Employees.ToList()
                           where emp1.Grade == "A"
                           select new Employee()
                           {
                               Name = emp1.Name,
                               Email = emp1.Email,
                               Address = emp1.Address,
                           }).ToList(); //LINQ

            var efEmp = db.Employees.Where(x => x.Grade == "B").ToList();

            //SelectMany
            var abc1 = linqEmpName.SelectMany(x => x);

            //OfType

            List<object> lstObj = new List<object>() { "ABC", 15, 11, 12, 88, "XYZ", "Demo" };

            var intData = lstObj.OfType<int>().ToList();

            //Except - check first DS with second DS and give result which is not in second DS

            //DS1 {1,2,3,4,5,6}
            //DS2 {1,4,5,6,7,8}
            //Output = 2,3
            var except = linqEmp.Except(efEmp).ToList(); //EF

            var linqExcept = (from ds1 in linqEmp
                              select ds1).Except(efEmp).ToList(); // Linq

            //Intersect
            //DS1 {1,2,3,4,5,6}
            //DS2 {1,4,5,6,7,8}
            //Output = 1,4,5,6

            var intersect = linqEmp.Intersect(efEmp).ToList(); //EF

            var linqIntersect = (from ds1 in linqEmp
                                 select ds1).Intersect(efEmp).ToList(); // Linq

            //Union
            //DS1 {1,2,3,4,5,6}
            //DS2 {1,4,5,6,7,8}
            //Output = 1,2,3,4,5,6,7,8

            var union = linqEmp.Union(efEmp).ToList(); //EF

            var linqUnion = (from ds1 in linqEmp
                             select ds1).Union(efEmp).ToList(); // Linq

            //Concat
            //DS1 {1,2,3,4,5,6}
            //DS2 {1,4,5,6,7,8}
            //Output = 1,2,3,4,5,6,1,4,5,6,7,8

            var concat = linqEmp.Concat(efEmp).ToList(); //EF

            var linqConcat = (from ds1 in linqEmp
                              select ds1).Concat(efEmp).ToList(); // Linq

            //OrderBy
            var orderBy = linqEmp.OrderBy(x => x.Name).ThenBy(x => x.Grade).ThenByDescending(x => x.EmpSeq); //EF
            var orderByDescending = linqEmp.OrderByDescending(x => x.Name).ThenBy(x => x.Grade).ThenByDescending(x => x.EmpSeq); //EF

            var linqEmp1 = (from emp1 in db.Employees.ToList()
                            where emp1.Grade == "A"
                            select new Employee()
                            {
                                Name = emp1.Name,
                                Email = emp1.Email,
                                Address = emp1.Address,
                            }).OrderByDescending(x => x.Name).ToList(); //LINQ

            //Reverse
            List<int?> ints = db.Employees.Select(x => x.EmpSeq).ToList();
            ints.Reverse();

            List<string> intss = db.Employees.Select(x => x.Name).ToList();
            intss.Reverse();


            //# Aggregate Operators --> Sum, Min, Max, Avg, Count

            var sumPrice = db.Products.Sum(x => x.Price);
            var avgPrice = db.Products.Average(x => x.Price);
            var minPrice = db.Products.Min(x => x.Price);
            var maxPrice = db.Products.Max(x => x.Price);
            var counts = db.Products.Count();

            //Any

            var anyemps = db.Employees.ToList();

            //if (anyProducts.Any()) //replacement of counts > 0
            if (anyemps.Any(x => x.Grade == "A")) //replacement of counts > 0
            {
                //logic
            }

            var anyempNames = db.Employees.Select(x => x.Name).ToList();
            string searchName = "Abc";
            anyempNames.Contains(searchName);

            db.Employees.FirstOrDefault(x => x.Grade == "A");
            db.Employees.First();
            //db.Employees.LastOrDefault();
            //db.Employees.Last();

            //LINQ END

            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("CreateNewRecord")]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}