using System.Linq;
using System.Web.Mvc;
using BIDV.Repository;
using PagedList;

namespace BIDV.Areas.mobile.Controllers
{
    public class NewsController : Controller
    { 
        readonly  NewsRepository _newsRepository = new NewsRepository();
        readonly TagRepository _tagRepository = new TagRepository();
        //
        // GET: /News/

        public ActionResult Index(int page = 1)
        {
            var listNews =
                _newsRepository.GetAll().Where(a => a.type == 0 && a.status == 1).OrderByDescending(a => a.created).ToList();
            return View("~/Areas/mobile/Views/News/Index.cshtml", listNews.ToPagedList(page, Config.PageSize));
        }
        public ActionResult Detail(int id)
        {
            var objNews =
                _newsRepository.GetById(id);
            var lstTagHot = _tagRepository.GetWhere(g => g.is_hot == 1 && g.status == 1).ToList();
            ViewBag.TagHot = lstTagHot;
            return View("~/Areas/mobile/Views/News/Detail.cshtml",objNews);
        }

       public ActionResult FindTag(string tagName, int page = 1)
       {

           var listNews =
               _newsRepository.GetAll().Where(a => a.status == 1 &&
                   (a.bidv__tag_items.Count(g => g.bidv__tags.tag.Contains(tagName)) > 0)).OrderByDescending(a => a.created).ToList();
           return View("~/Areas/mobile/Views/News/FindTag.cshtml",listNews.ToPagedList(page, Config.PageSize));
       }
       public ActionResult HotTag(string tagName, int page = 1)
       {

           var listNews =
               _newsRepository.GetAll().Where(a => a.status == 1 &&
                   (a.bidv__tag_items.Count(g => g.bidv__tags.tag.Contains(tagName) && g.bidv__tags.is_hot == 1 && g.bidv__tags.status == 1) > 0)).OrderByDescending(a => a.created).ToList();
           return View("~/Areas/mobile/Views/News/HotTag.cshtml", listNews.ToPagedList(page, Config.PageSize));
       }
    }
}
