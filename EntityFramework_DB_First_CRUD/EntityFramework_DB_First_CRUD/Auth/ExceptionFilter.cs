using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace EntityFramework_DB_First_CRUD.Auth
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var cName = filterContext.RouteData.Values["controller"];
            var aName = filterContext.RouteData.Values["action"];
            var errorMsg = filterContext.Exception.Message;
            var errorStackTrace = filterContext.Exception.StackTrace;

            Log("Execption occurred in controller : " + cName + " and acion method :" + aName + " and error message :" + errorMsg + " and strak trace : " + errorStackTrace);

            filterContext.ExceptionHandled = true;

            filterContext.Result = new RedirectResult("~/Error/ErrorIndex");
            //filterContext.Result = new ViewResult { ViewName = "ErrorIndex" };
        }

        private void Log(string msg)
        {
            string m_exePath = @"C:\Users\BANSIDHARA\source\repos\MVC Repo\ShowITMVC\EntityFramework_DB_First_CRUD\EntityFramework_DB_First_CRUD\";
            File.AppendAllText(m_exePath + "log.txt", "----------------------------------------------------------------------------------------------------------------------------------------");

            //write log custom code
            File.AppendAllText(m_exePath + "log.txt", msg);
        }
    }
}