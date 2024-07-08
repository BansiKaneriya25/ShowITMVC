using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Web_API_Auth.Auth
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.CompletedTask;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (var db = new ShowDotNetITEntities())
            {
                var username = context.UserName;
                var password = context.Password;

                var user = db.Users.Where(x => x.Username.Equals(username) && x.Password.Equals(password)).FirstOrDefault();

                if (user != null)
                {
                    var roles = db.UserRoles.Where(x => x.UserId == user.UserId).ToList();

                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("Age", "16"));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
                    foreach (var item in roles)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, item.Role.RoleName));
                    }
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("Invalie", "Invalid username password");
                    context.Rejected();
                }
            }
        }
    }
}