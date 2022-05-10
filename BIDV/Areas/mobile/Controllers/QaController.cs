using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Controllers;
using BIDV.Repository;

namespace BIDV.Areas.mobile.Controllers
{
    public class QaController : BaseController
    {
        //
        // GET: /mobile/Qa/
        readonly CategoryRepository _categoryRepository = new CategoryRepository();
        readonly QaRepository _qaRepository = new QaRepository();
        readonly FeatureRepository _featureRepository = new FeatureRepository();
        public ActionResult Index()
        {
            var lstCat = _categoryRepository.GetWhere(g => g.type == (int)Config.TypeCategory.Qa);
            ViewBag.ListCat = lstCat.ToList();
            var objFeature =
                _featureRepository.GetWhere(g => g.status == 1 && g.show == 1 && g.url_show == "faqs")
                    .OrderBy(g => g.weight)
                    .FirstOrDefault();
            return View("~/Areas/mobile/Views/Qa/Index.cshtml", objFeature);
        }
        public ActionResult ListData(int id)
        {
            var lstQa = _qaRepository.GetAll();
            var lstCatId = new List<int> { id };
            var lstChild = _categoryRepository.GetWhere(g => g.parent_id == id).ToList();
            if (lstChild.Any())
            {
                lstCatId.AddRange(lstChild.Select(g => g.id));
            }
            if (id > 0)
            {
                lstQa = lstQa.Where(g => g.cat_id != null && lstCatId.Contains(g.cat_id.Value));
            }
            lstQa = lstQa.OrderByDescending(g => g.created);
            return Json(RenderViewToString("~/Views/Qa/_ListData.cshtml", lstQa), JsonRequestBehavior.AllowGet);
        }
    }
}
