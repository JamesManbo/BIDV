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
    public class HomeController : Controller
    {
        readonly FeatureRepository _featureRepository = new FeatureRepository();
        readonly CategoryRepository _categoryRepository = new CategoryRepository();
        readonly PromotionRepository _promotionRepository = new PromotionRepository();
        readonly NewsRepository _newsRepository = new NewsRepository();
        public ActionResult Index()
        {
            var now = HelperDateTime.Convert2TimeStamp(DateTime.Now);
            var objFeature =
                _featureRepository.GetWhere(g => g.status == 1 && (g.url_show == "home" || g.url_show == "/")).OrderByDescending(g => g.created).FirstOrDefault();
            //Lấy tin Khuyến mãi
            var lstCatPromotionId = _categoryRepository.GetWhere(g => g.type == 3).Select(g => g.id).ToList();
            if (lstCatPromotionId.Any())
            {
                var lstNewsPromotion =
                    _newsRepository.GetWhere(g => lstCatPromotionId.Contains(g.cat_id.Value) && g.status == 1 && now > g.time_from && now < g.time_to && g.is_home == 1)
                        .OrderByDescending(g => g.created)
                        .Take(10);
                ViewBag.NewsPromotion = lstNewsPromotion.ToList();
            }
            //Lấy tin Điểm ưu đãi vàng
            var objCatFavorableId = _categoryRepository.GetWhere(g => g.type == 2).Select(g => g.id).ToList();
            if (objCatFavorableId.Any())
            {
                var lstNewsFavorable =
                    _promotionRepository.GetWhere(g => objCatFavorableId.Contains(g.cat_id) && g.status == 1 && now > g.time_from && now < g.time_to && g.is_home == 1)
                        .OrderByDescending(g => g.created)
                        .Take(10);
                ViewBag.NewsFavorable = lstNewsFavorable.ToList();
            }
            return View(objFeature);
        }

        public ActionResult ListNewsInHome()
        {
            var lstNews = _newsRepository.GetWhere(g => g.status == 1 & g.type == 0&&g.is_home==1).OrderByDescending(g => g.dateof).Take(Config.PageSize);
            if (HttpContext.Request.Browser.IsMobileDevice)
            {
                return View("~/Areas/mobile/Views/Home/ListNewsInMHome.cshtml", lstNews.ToList());
            }
            else
            {
                return View(lstNews);
            }
        }

        public ActionResult Search(string search_key, int page = 1)
        {
            var searchResult =
                _newsRepository.GetAll()
                    .Where(a => a.title.Contains(search_key) || a.sort_body.Contains(search_key) || a.body.Contains(search_key) && a.status == 1 && a.type == 0);
            ViewBag.Key = search_key;
            var bidvNewses = searchResult as IList<bidv__news> ?? searchResult.ToList();
            ViewBag.Count = bidvNewses.Count();
            searchResult = bidvNewses.OrderByDescending(a => a.created);
            return View(searchResult.ToPagedList(page, Config.PageSize));
        }
    }
}
