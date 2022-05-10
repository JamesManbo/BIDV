using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Common;
using BIDV.Repository;
using PagedList;

namespace BIDV.Controllers
{
    public class NewsController : Controller
    {
        readonly NewsRepository _newsRepository = new NewsRepository();
        readonly TagRepository _tagRepository = new TagRepository();
        readonly FeatureRepository _featureRepository = new FeatureRepository();
        //
        // GET: /News/

        public ActionResult Index(int page = 1)
        {
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            var listNews =
                _newsRepository.GetAll().Where(a => a.type == 0 && a.status == 1 && a.dateof <= HelperDateTime.Convert2TimeStamp(DateTime.Now) && a.cat_id != 12).OrderByDescending(a => a.dateof).ToList();
            var objFeature =
                _featureRepository.GetWhere(g => g.url_show == "/News" && g.start <= timestamp && g.end >= timestamp).OrderByDescending(g=>g.created).FirstOrDefault();
            TempData["Feature"] = objFeature;
            return View(listNews.ToPagedList(page, Config.PageSize));
        }
        public ActionResult Detail(int id)
        {
            var objNews =
                _newsRepository.GetById(id);
            objNews.view += 1;
            _newsRepository.Update(objNews);
            var lstTagHot = _tagRepository.GetWhere(g => g.is_hot == 1 && g.status == 1).ToList();
            ViewBag.TagHot = lstTagHot;
            return View(objNews);
        }

        public ActionResult FindTag(string tagName, int page = 1)
        {

            var listNews =
                _newsRepository.GetAll().Where(a => a.status == 1 &&
                    (a.bidv__tag_items.Count(g => g.bidv__tags.tag.Contains(tagName)) > 0)).OrderByDescending(a => a.dateof).ToList();
            return View(listNews.ToPagedList(page, Config.PageSize));
        }
        public ActionResult HotTag(string tagName, int page = 1)
        {

            var listNews =
                _newsRepository.GetAll().Where(a => a.status == 1 &&
                    (a.bidv__tag_items.Count(g => g.bidv__tags.tag.Contains(tagName) && g.bidv__tags.is_hot == 1 && g.bidv__tags.status == 1) > 0)).OrderByDescending(a => a.dateof).ToList();
            return View(listNews.ToPagedList(page, Config.PageSize));
        }
    }
}
