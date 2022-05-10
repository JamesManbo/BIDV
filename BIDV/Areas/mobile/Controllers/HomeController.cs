using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Common;
using BIDV.Repository;

namespace BIDV.Areas.mobile.Controllers
{
    public class HomeController : Controller
    {
        readonly FeatureRepository _featureRepository = new FeatureRepository();
        readonly CategoryRepository _categoryRepository = new CategoryRepository();
        readonly PromotionRepository _promotionRepository = new PromotionRepository();
        readonly NewsRepository _newsRepository = new NewsRepository();
        //
        // GET: /mobile/Home/

        public ActionResult Index()
        {
            var now = HelperDateTime.Convert2TimeStamp(DateTime.Now);
            var objFeature =
                _featureRepository.GetWhere(g => g.status == 1 && g.url_show == "home").OrderBy(g => g.weight).FirstOrDefault();
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
    }
}
