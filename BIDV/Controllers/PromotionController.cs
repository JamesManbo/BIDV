using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Common;
using BIDV.Model;
using BIDV.Repository;
using PagedList;
using BIDV.Controllers;

namespace BIDV.Content
{
    public class PromotionController : Controller
    {
        readonly PromotionRepository _promotionRepository = new PromotionRepository();
        readonly NewsRepository _newsRepository = new NewsRepository();
        readonly CategoryRepository _categoryRepository = new CategoryRepository();
        readonly FeatureRepository _featureRepository = new FeatureRepository();
        //
        // GET: /Promotion/

        public ActionResult GoldPromotion(int? sort, int? card, int? promo_per, int? cityId, int? cat, int page = 1)
        {
            ViewBag.sort = sort;
            ViewBag.card = card;
            ViewBag.promo_per = promo_per;
            ViewBag.cityId = cityId;
            ViewBag.cat = cat;
            var promotionList = _promotionRepository.GetAll().Where(a => a.status == 1 && a.created > 0 && HelperDateTime.Convert2TimeStamp(DateTime.Now) >= a.time_from && HelperDateTime.Convert2TimeStamp(DateTime.Now) <= a.time_to);
            if (sort == 1)
            {
                promotionList = promotionList.OrderByDescending(g => g.created);
            }
            else if (sort == 2)
            {
                promotionList = promotionList.OrderBy(g => g.created);
            }
            if (card > 0)
            {
                promotionList = promotionList.Where(g =>
                {
                    var bidvPromotionCard = g.bidv__promotion_card.FirstOrDefault();
                    return bidvPromotionCard != null && bidvPromotionCard.card_id == card;
                });
            }
            if (promo_per != -1 && promo_per != null)
            {
                promotionList = promotionList.Where(g => g.promo_per == promo_per);
            }
            if (cat > 0)
            {
                promotionList = promotionList.Where(g => g.cat_id == cat);
            }
            if (cityId > 0)
            {
                promotionList =
                    promotionList.Where(a =>
                    {
                        var bidvPromotionCity = a.bidv__promotion_city.FirstOrDefault();
                        return bidvPromotionCity != null && bidvPromotionCity.city_id == cityId;
                    });
            }
            promotionList = promotionList.OrderByDescending(g => g.created);
            var lstCategory = _categoryRepository.GetWhere(g => g.type == 2);
            ViewBag.ListCategory = lstCategory.ToList();
            return View(promotionList.ToPagedList(page, Config.PageSize));
        }
        public ActionResult Index(int page = 1)
        {
            var objFeature = _featureRepository.GetWhere(g => g.status == 1 && g.url_show == "/Promotion").OrderByDescending(g => g.created).FirstOrDefault();
            TempData["Feature"] = objFeature;
            var listPromotionNews =
                _newsRepository.GetAll()
                    .Where(
                        a =>
                            a.time_from <= HelperDateTime.Convert2TimeStamp(DateTime.Now) &&
                            a.time_to >= HelperDateTime.Convert2TimeStamp(DateTime.Now) && a.status == 1);
            //var listPromotionNews = _newsRepository.GetAll();
            listPromotionNews = listPromotionNews.OrderByDescending(a => a.created);
            return View(listPromotionNews.ToPagedList(page, Config.PageSize));
        }
        public ActionResult PromotionDetail(int id)
        {
            var objNews =
                _promotionRepository.GetById(id);
            objNews.view = objNews.view ?? 0;
            objNews.view += 1;
            _promotionRepository.Update(objNews);
            var lstHotPromotion = _promotionRepository.GetAll().Where(g => g.is_hot == 1 && g.status == 1 && g.time_from <= HelperDateTime.Convert2TimeStamp(DateTime.Now) && g.time_to >= HelperDateTime.Convert2TimeStamp(DateTime.Now)).OrderByDescending(g => g.created).Take(4).ToList();
            //var lstHotPromotion = _promotionRepository.GetAll().Where(g => g.is_hot == 1 && g.status == 1).OrderByDescending(g => g.created).Take(4).ToList();
            ViewBag.PromotionHot = lstHotPromotion;
            return View(objNews);
        }
        public ActionResult NewsPromotionDetail(int id)
        {
            var objNews =
                _newsRepository.GetById(id);
            objNews.view = objNews.view ?? 0;
            objNews.view += 1;
            _newsRepository.Update(objNews);
            return View(objNews);
        }
    }
}
