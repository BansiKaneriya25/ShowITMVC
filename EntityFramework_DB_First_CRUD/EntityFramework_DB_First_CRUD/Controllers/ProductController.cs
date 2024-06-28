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
            //Pagination used for take and skip
            //int PageNumber = 1;
            //int RecordPerPage = 10;
            //do
            //{
            //    if (PageNumber < 2)
            //    {
            //        var empPagination = db.Employees.ToList()
            //              .Skip(PageNumber - 1 * RecordPerPage) //pagenumer - 1 * recordperpage
            //              .Take(RecordPerPage);
            //        PageNumber++;
            //    }
            //} while (true);





            //IEnumerable 
            var empIEnumerable = db.Employees.Where(x => x.IsActive).AsEnumerable();
            empIEnumerable = empIEnumerable.Take(2);
            //select * from employee where isactive = 1
            //select top 2 * from employee where isactive = 1

            //IQueryable
            var empIQueryable = db.Employees.AsQueryable().Where(x => x.IsActive);
            empIQueryable = empIQueryable.Take(2);
            //select top 2 * from employee where isactive = 1

            //Group By - EF
            var empGroupBy = db.Employees.GroupBy(x => x.Grade).OrderByDescending(x => x.Key)
                .Select(x => new EmpDetails { Key = x.Key, Employees = x.OrderByDescending(z => z.Name).ToList() })
                //.Select(x => new { Key = x.Key, Employees = x.OrderByDescending(z => z.Name).ToList() })
                //.Select(x => new { Employees = x.OrderByDescending(z => z.Name).ToList() })
                .ToList();

            //Group By - Linq
            var empLinq = from emplinq in db.Employees.ToList()
                          group emplinq by emplinq.Grade into empGrade
                          orderby empGrade.Key descending
                          select new EmpDetails
                          {
                              Key = empGrade.Key,
                              FullName = empGrade.FirstOrDefault().Name,
                              MobileNumber = empGrade.FirstOrDefault().PhoneNumber,
                              Address1 = empGrade.FirstOrDefault().Address,
                              Employees = empGrade.OrderBy(x => x.Name).ToList()
                          };

            //All column data return
            var empLinq1 = from emplinq in db.Employees.ToList()
                           group emplinq by emplinq.Grade into empGrade
                           orderby empGrade.Key descending
                           select empGrade; // anonymouse


            //Group By with Multiple Key - EF
            var empEF_mulKey = db.Employees.ToList().GroupBy(x => (x.Grade, x.Gender))
                                .Where(x => x.Key.Grade.Equals("A") && x.Key.Gender.Equals("M"))
                                .Select(y => new
                                {
                                    Grade = y.Key.Grade,
                                    Gender = y.Key.Gender,
                                    Employees = y.OrderBy(z => z.Name)
                                });

            // Get A grade male and female employee count
            int? M_AGradeCount = empEF_mulKey.Where(x => x.Grade == "A" && x.Gender == "M").FirstOrDefault().Employees.Count();
            int? F_AGradeCount = empEF_mulKey.Where(x => x.Grade == "A" && x.Gender == "F").FirstOrDefault()?.Employees.Count();

            //Group By with Multiple Key - Linq
            var empLinq_mulKey = from emplinq in db.Employees.ToList()
                                 group emplinq by (emplinq.Grade, emplinq.Gender) into empGroup
                                 orderby empGroup.Key.Gender descending
                                 select new EmpDetails
                                 {
                                     Gender = empGroup.Key.Gender,
                                     Grade = empGroup.Key.Grade,
                                     FullName = empGroup.FirstOrDefault().Name,
                                     MobileNumber = empGroup.FirstOrDefault().PhoneNumber,
                                     Address1 = empGroup.FirstOrDefault().Address,
                                     Employees = empGroup.OrderBy(x => x.Name).ToList()
                                 };

            //return top 2 M employee of A grade employee
            var take2Emp = db.Employees.ToList().Where(x => x.Grade.Equals("A")).OrderBy(x => x.Name).Take(2);

            var skipEmp = db.Employees.ToList().Skip(2);

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

            //Joins

            //Left Join - Linq Query
            var empLeftJoin = from empLJ in db.Employees.ToList() // Left table data source
                              join empDetailLJ in db.EmployeeDetails.ToList() // Right table data source
                              on empLJ.EmployeeId equals empDetailLJ.EmployeeId // Join column condition
                              into empDetailsData //Performing linq group set
                              from EmployeeDetail in empDetailsData.DefaultIfEmpty() //Performing left join
                              select new { empLJ, EmployeeDetail }; // Projecting result to anonymous type

            //Inner Join - Linq Query
            var empInnerJoin = from empLJ in db.Employees.ToList() // Left table data source
                               join empDetailLJ in db.EmployeeDetails.ToList() // Right table data source
                               on empLJ.EmployeeId equals empDetailLJ.EmployeeId // Join column condition
                               //into empDetailsData //Performing linq group set
                               //from EmployeeDetail in empDetailsData //Performing inner join
                               select new { empLJ, empDetailLJ }; // Projecting result to anonymous type

            var empEFInnerJoin = db.Employees.ToList() //Outer Data Source
                                    .Join(db.EmployeeDetails.ToList(), //Inner Data Source
                                        employee => employee.EmployeeId, //Outer Key Selector
                                        empDetails => empDetails.EmployeeId, //Inner Key Selector
                                        (employee, empDetails) => new EmpDetails1() //Projecting the data into result set
                                        {
                                            Employees = employee,
                                            EmployeeDetails = empDetails,
                                            FullName = employee.Name + " " + employee.Name
                                        }).ToList();

            //Inner Join - Linq Query with Multiple data source
            var empInnerJoinMultipleDataSource = from empLJ in db.Employees.ToList() // Left table data source
                                                 join empDetailLJ in db.EmployeeDetails.ToList() // Right table data source
                                                 on empLJ.EmployeeId equals empDetailLJ.EmployeeId // Join column condition
                                                 join empCategory in db.EmployeeCategories.ToList() // Right table data source
                                                 on empLJ.EmpCategoryId equals empCategory.EmpCategoryId // Join column condition
                                                 select new { empLJ, empDetailLJ, empCategory }; // Projecting result to anonymous type
            
            //Inner Join - EF Query with Multiple data source
            var empEFInnerJoinMultipleDataSource = db.Employees.ToList() //Outer Data Source
                                                    .Join(db.EmployeeDetails.ToList(), //Inner Data Source
                                                    employee => employee.EmployeeId, //Outer Key Selector
                                                    empDetails => empDetails.EmployeeId, //Inner Key Selector
                                                    (employee, empDetails) => new { employee, empDetails })
                                                    .Join(db.EmployeeCategories.ToList(), //Inner Data Source 2
                                                    employee => employee.employee.EmpCategoryId, //Outer Key Selector
                                                    empCategory => empCategory.EmpCategoryId, //Inner Key Selector 2
                                                    (employee, empCategory) => new { employee, empCategory })
                                                    .Select(x => new EmpDetails1() //Projecting the data into result set
                                                    {
                                                        Employees = x.employee.employee,
                                                        EmployeeDetails = x.employee.empDetails,
                                                        FullName = x.employee.employee.Name + " " + x.employee.employee.Name
                                                    }).ToList();
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