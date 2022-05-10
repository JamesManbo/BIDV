using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDV.BaseSecurity
{
    //[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    //public class CustomAuthorize: AuthorizeAttribute
    //{
    //    public CustomAuthorize(params object[] roles)
    //    {
    //        if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
    //            throw new ArgumentException("roles");
    //        this.Roles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
    //        //this.Roles = string.Join(",", roles.Select(r => (int)r));
    //    }
    //}

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorize : AuthorizeAttribute
    {
        private string[] UserProfilesRequired { get; set; }

        public CustomAuthorize(params object[] userProfilesRequired)
        {
            if (userProfilesRequired.Any(p => p.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("userProfilesRequired");

            this.UserProfilesRequired = userProfilesRequired.Select(p => Enum.GetName(p.GetType(), p)).ToArray();
        }

        public override void OnAuthorization(AuthorizationContext context)
        {
            bool authorized = false;
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var url = new UrlHelper(context.RequestContext);
                var logonUrl = url.Action("Login", "Account");
                context.Result = new RedirectResult(logonUrl);
            }
            else
            {
                foreach (var role in this.UserProfilesRequired)
                    if (HttpContext.Current.User.IsInRole(role))
                    {
                        authorized = true;
                        break;
                    }

                if (!authorized)
                {
                    var url = new UrlHelper(context.RequestContext);
                    var logonUrl = url.Action("Http", "Error", new { Id = 401, Area = "" });
                    context.Result = new RedirectResult(logonUrl);
                }
            }
            
        }
    }
}