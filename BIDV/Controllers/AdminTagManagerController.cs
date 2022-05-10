using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Common;
using BIDV.Model;
using BIDV.Repository;
using PagedList;

namespace BIDV.Controllers
{
    [Authorize]
    public class AdminTagManagerController : Controller
    {
        //
        // GET: /AdminTagManager/
        readonly TagRepository _tagRepository = new TagRepository();
        public ActionResult Index(string title, int? status, int? is_hot, int page = 1)
        {
            ViewBag.tag = title;
            ViewBag.status = status;
            ViewBag.is_hot = is_hot;
            var lstTags = _tagRepository.GetAll();
            if (!string.IsNullOrEmpty(title))
            {
                lstTags =
                    lstTags.Where(
                        g =>
                            HelperString.UnsignCharacter(g.title.ToLower().Trim())
                                .Contains(HelperString.UnsignCharacter(title.ToLower().Trim())));
            }
            if (status != -2 && status != null)
            {
                lstTags = lstTags.Where(g => g.status != null && g.status.Value == status);
            }
            if (is_hot != -2 && is_hot != null)
            {
                lstTags = lstTags.Where(g => g.is_hot != null && g.is_hot.Value == is_hot);
            }
            lstTags = lstTags.OrderByDescending(g => g.created);
            return View(lstTags.ToPagedList(page, 20));
        }

        public ActionResult Edit(int id)
        {
            var obj = _tagRepository.GetById(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(bidv__tags item)
        {
            var obj = _tagRepository.GetById(item.id);
            obj.status = item.status;
            obj.is_hot = item.is_hot;
            _tagRepository.Update(obj);
            return RedirectToAction("Index", "AdminTagManager", new { page = 1 });
        }

        public ActionResult Delete(int id)
        {
            var obj = _tagRepository.GetById(id);
            obj.status = -1;
            _tagRepository.Update(obj);
            return RedirectToAction("Index", "AdminTagManager", new { page = 1 });
        }
    }
}
