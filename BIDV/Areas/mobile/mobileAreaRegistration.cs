using System.Web.Mvc;

namespace BIDV.Areas.mobile
{
    public class mobileAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "mobile";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //                "mobile_default",
            //                "mobile/{controller}/{action}/{id}",
            //                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //                new[] { "BIDV.Areas.mobile.Controllers" }
            //            );
            context.Routes.Add("mobile_default", new SubDomainRoute(
                "{controller}/{action}/{id}",
                new { area = "mobile", controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { typeof(Controllers.HomeController).Namespace } // Namespaces defaults
            ));
        }
    }
}
