using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.BaseSecurity;

namespace BIDV.Controllers
{
    public class BaseController : Controller
    {
        protected virtual new CustomPrincipal User
        {
            get
            {
                return HttpContext.User as CustomPrincipal;
            }
        }
        ///Gọi vào partialview this.RenderRazorViewToString("ViewName");
        public string RenderRazorViewToString(string viewName)
        {
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        ///Gọi vào partialview this.RenderViewToString(this.ControllerContext, "ViewtName", null);
        public string RenderViewToString(ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");

            ViewDataDictionary viewData = new ViewDataDictionary(model);

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                ViewContext viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        /// Gọi vào partialview this.RenderViewToString(this.ControllerContext, "ViewtName", null);
        public string RenderViewToString(ControllerContext context, string viewName, object model, TempDataDictionary tempData)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");

            ViewDataDictionary viewData = new ViewDataDictionary(model);

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                ViewContext viewContext = new ViewContext(context, viewResult.View, viewData, tempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public string RenderViewToString(string viewName, object model = null)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            using (var sw = new StringWriter())
            {
                var result = PartialView(viewName);
                result.View = ViewEngines.Engines.FindPartialView(ControllerContext, viewName).View;
                if (model != null)
                {
                    result.ViewData = new ViewDataDictionary(model);
                }

                ViewContext vc = new ViewContext(ControllerContext, result.View, result.ViewData, result.TempData, sw);

                result.View.Render(vc, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
