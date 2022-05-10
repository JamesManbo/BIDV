using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Common;
using BIDV.Repository;
using PagedList;

namespace BIDV.Content
{
    public class PromotionMobileController : Controller
    {
        readonly PromotionRepository _promotionRepository = new PromotionRepository();
        readonly NewsRepository _newsRepository = new NewsRepository();
        readonly CategoryRepository _categoryRepository = new CategoryRepository();
        //
        // GET: /Promotion/

        public ActionResult GoldPromotion(int? sort, int? card, int? promoPer, int? cityId, int? cat, int page = 1)
        {
            var promotionList = _promotionRepository.GetAll().Where(a => a.status == 1 && a.created > 0 && HelperDateTime.Convert2TimeStamp(DateTime.Now) >= a.time_from && HelperDateTime.Convert2TimeStamp(DateTime.Now) <= a.time_to);
            if (sort == 1)
            {
                promotionList = promotionList.OrderByDescending(g => g.created);
            }
            else if (sort == 2)
            {
                promotionList = promotionList.OrderBy(g => g.created);
            }
            if (card >= 0)
            {
                promotionList = promotionList.Where(g => g.cat_id == card);
            }
            if (promoPer >= 0)
            {
                promotionList = promotionList.Where(g => g.promo_per == promoPer);
            }
            if (cat >= 0)
            {
                promotionList = promotionList.Where(g => g.cat_id == cat);
            }
            if (cityId >= 0)
            {
                promotionList =
                    promotionList.Where(a =>
                    {
                        var bidvPromotionCity = a.bidv__promotion_city.FirstOrDefault();
                        return bidvPromotionCity != null && bidvPromotionCity.city_id == cityId;
                    });
            }
            promotionList = promotionList.OrderByDescending(g => g.created);
            
            return View("~/Areas/mobile/Views/Promotion/GoldPromotion.cshtml", promotionList.ToPagedList(page, Config.PageSize));
        }
        public ActionResult Index(int page = 1)
        {

            var listPromotionNews =
                _newsRepository.GetAll()
                    .Where(
                        a =>
                            a.time_from <= HelperDateTime.Convert2TimeStamp(DateTime.Now) &&
                            a.time_to >= HelperDateTime.Convert2TimeStamp(DateTime.Now));
            return View("~/Areas/mobile/Views/Promotion/Index.cshtml", listPromotionNews.ToPagedList(page, Config.PageSize));
        }
        public ActionResult PromotionDetail(int id)
        {
            var objNews =
                _promotionRepository.GetById(id);
            var lstHotPromotion = _promotionRepository.GetAll().Where(g => g.is_hot == 1 && g.status == 1 && g.time_from <= HelperDateTime.Convert2TimeStamp(DateTime.Now) && g.time_to >= HelperDateTime.Convert2TimeStamp(DateTime.Now)).ToList();
            ViewBag.PromotionHot = lstHotPromotion;
            return View("~/Areas/mobile/Views/Promotion/PromotionDetail.cshtml", objNews);
        }
        public ActionResult NewsPromotionDetail(int id)
        {
            var objNews =
                _newsRepository.GetById(id);
            
            return View("~/Areas/mobile/Views/Promotion/NewsPromotionDetail.cshtml", objNews);
        }
    }
}
