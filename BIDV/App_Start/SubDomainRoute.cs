using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BIDV
{
    public class SubDomainRoute : Route
    {
        private readonly string[] namespaces;

        public SubDomainRoute(string url, object defaults, string[] namespaces)
            : base(url, new RouteValueDictionary(defaults), new MvcRouteHandler())
        {
            this.namespaces = namespaces;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            //HelperCache.ClearAllCache();
            var ismobile = httpContext.Request.Browser.IsMobileDevice;
            var routeData = base.GetRouteData(httpContext);
            if (routeData != null)
            {
                if (ismobile)
                {
                    namespaces.SetValue("BIDV.Areas.mobile.Controllers", 0);
                    if (this.namespaces != null && this.namespaces.Length > 0)
                    {
                        routeData.DataTokens["Namespaces"] = this.namespaces;
                    }
                    routeData.Values["area"] = "mobile";
                    routeData.DataTokens["Area"] = "mobile";
                }
                else
                {
                    return null;
                }

                routeData.DataTokens["UseNamespaceFallback"] = bool.FalseString;
                return routeData;
            }
            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}