using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Repository;

namespace BIDV.Controllers
{
    public class QaController : BaseController
    {
        //
        // GET: /Qa/
        readonly CategoryRepository _categoryRepository = new CategoryRepository();
        readonly QaRepository _qaRepository = new QaRepository();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListCategory()
        {
            var lstCat = _categoryRepository.GetWhere(g => g.type == 4);
            return View(lstCat);
        }

        public ActionResult ListData(int id)
        {
            var lstQa = _qaRepository.GetAll();
            var lstCatId = new List<int> {id};
            var lstChild = _categoryRepository.GetWhere(g => g.parent_id == id).ToList();
            if (lstChild.Any())
            {
                lstCatId.AddRange(lstChild.Select(g=>g.id));
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
