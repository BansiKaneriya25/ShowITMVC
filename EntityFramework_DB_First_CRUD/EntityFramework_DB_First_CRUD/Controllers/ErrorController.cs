using EntityFramework_DB_First_CRUD.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFramework_DB_First_CRUD.Controllers
{
    [LogActionFilter]
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult ErrorIndex()
        {
            return View();
        }
    }
}