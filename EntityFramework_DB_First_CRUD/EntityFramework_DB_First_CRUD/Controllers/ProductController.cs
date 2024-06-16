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
            //var abc = db.Products.SqlQuery("select * from Products where Price < 15").ToList();
            
            //Lazy Laoding
            var list = db.Products.ToList(); // 10000 - 20 sec
            //Business
            var abc = list.Where(x => x.Price < 15).ToList(); // min 1 sec // NO DB CONNECTION

            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost,ActionName("CreateNewRecord")]
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