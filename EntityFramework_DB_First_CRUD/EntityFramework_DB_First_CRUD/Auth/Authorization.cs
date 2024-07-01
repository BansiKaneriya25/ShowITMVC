using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFramework_DB_First_CRUD.Auth
{
    public class Authorization : FilterAttribute, IAuthorizationFilter
    {
        private ShowDotNetITEntities db = new ShowDotNetITEntities();

        private string[] _roles;
        public Authorization(params string[] role)
        {
            _roles = role;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var username = Convert.ToString(filterContext.HttpContext.Session["Username"]);
            var password = Convert.ToString(filterContext.HttpContext.Session["Password"]);

            var checkUser = db.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();

            if (checkUser == null)
            {
                filterContext.Result = new RedirectResult("~/Account/Logout");
            }
            else
            {
                var userRoles = db.UserRoles.Where(x => x.UserId == checkUser.UserId).ToList();

                if (_roles.Count() > 0)
                {
                    var roles = _roles.FirstOrDefault().Split(',');

                    string[] userRoleNames = userRoles.Select(x => x.Role.RoleName).ToArray();

                    var checkRights = roles.Intersect(userRoleNames).Count() > 0;

                    if (!checkRights)
                    {
                        filterContext.Result = new RedirectResult("~/Error/AccessDenied");
                    }
                }
            }
        }
    }
}