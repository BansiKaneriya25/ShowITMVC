using EntityFramework_DB_First_CRUD.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFramework_DB_First_CRUD.Controllers
{
    [LogActionFilter]
    public class AccountController : Controller
    {
        private ShowDotNetITEntities db = new ShowDotNetITEntities();

        [AllowAnonymous]
        [LogResultFilter]
        public ActionResult Login()//2
        {
            //int a = 0;
            //var aaa = 1 / a;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                //Validation
                var userDetails = db.Users.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();

                if (userDetails != null)
                {
                    //Store in Session

                    Session["Username"] = user.Username;
                    Session["Password"] = user.Password;

                    return RedirectToAction("Index", "Product");
                }
                ViewBag.Validation = "Invalide credentials...";
                return View(user);
            }
            return View(user);
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        [Authorization]
        public ActionResult Index()
        {
            return View();
        }
    }
}