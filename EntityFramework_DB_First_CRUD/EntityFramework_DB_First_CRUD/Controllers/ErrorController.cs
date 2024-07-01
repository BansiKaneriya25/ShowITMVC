using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFramework_DB_First_CRUD.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}