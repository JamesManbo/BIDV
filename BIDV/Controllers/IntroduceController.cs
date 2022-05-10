using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Repository;

namespace BIDV.Controllers
{
    public class IntroduceController : Controller
    {
        //
        // GET: /Introduce/
        readonly FeatureRepository _featureRepository = new FeatureRepository();
        public ActionResult Index()
        {
            var objFeature =
                 _featureRepository.GetWhere(g => g.status == 1 && g.show == 1 && g.url_show == "home")
                     .OrderBy(g => g.weight)
                     .FirstOrDefault();
            return View(objFeature);
        }
    }
}
