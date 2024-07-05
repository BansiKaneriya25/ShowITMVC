using EntityFramework_DB_First_CRUD.Auth;
using System.Web;
using System.Web.Mvc;

namespace EntityFramework_DB_First_CRUD
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionFilter());
        }
    }
}
