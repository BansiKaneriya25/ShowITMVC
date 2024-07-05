using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFramework_DB_First_CRUD.Auth
{
    public class LogActionFilter : FilterAttribute, IActionFilter
    {
        //Before//1
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("Action Method Executing: " + filterContext.ActionDescriptor.ActionName);
        }

        //3
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("Action Method Executed: " + filterContext.ActionDescriptor.ActionName);
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