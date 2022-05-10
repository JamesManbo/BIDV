using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BIDV.Common;
using BIDV.Model;
using BIDV.Repository;
using PagedList;

namespace BIDV.Controllers
{
    [Authorize]
    public class AdminGoldPromoController : Controller
    {
        //
        // GET: /AdminGoldPromo/
        readonly PromotionRepository _promotionRepository = new PromotionRepository();
        readonly CategoryRepository _categoryRepository = new CategoryRepository();
        readonly ProvinceRepository _provinceRepository = new ProvinceRepository();
        readonly PromoDesRepository _promoDesRepository = new PromoDesRepository();
        readonly PromoCardRepository _promoCardRepository = new PromoCardRepository();
        readonly PromoCardCityRepository _promoCardCityRepository = new PromoCardCityRepository();
        readonly PromoImageRepository _promoImageRepository = new PromoImageRepository();
        public ActionResult Index(int? status, int? cat_id, string fromdate, string todate, string title, int page = 1)
        {
            var lstGoldPromo = _promotionRepository.GetAll();
            var now = HelperDateTime.Convert2TimeStamp(DateTime.Now);
            if (status != null && status != -2)
            {
                if (status == 3)
                {
                    lstGoldPromo = lstGoldPromo.Where(g => g.time_to < now);
                }
                else if (status == 1)
                {
                    lstGoldPromo = lstGoldPromo.Where(g => g.time_to >= now);
                }
                else
                {
                    lstGoldPromo = lstGoldPromo.Where(g => g.status == status);
                }
            }
            else
            {
                lstGoldPromo = lstGoldPromo.Where(g => g.status != -1);
            }
            if (cat_id != null && cat_id != 0)
            {
                lstGoldPromo = lstGoldPromo.Where(g => g.cat_id == cat_id);
            }
            if (!string.IsNullOrEmpty(fromdate))
            {
                lstGoldPromo =
                    lstGoldPromo.Where(g => HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(fromdate)) <= g.created);
            }
            if (!string.IsNullOrEmpty(todate))
            {
                lstGoldPromo =
                    lstGoldPromo.Where(g => HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(todate)) >= g.created);
            }
            if (!string.IsNullOrEmpty(title))
            {
                lstGoldPromo =
                    lstGoldPromo.Where(
                        g =>
                            HelperString.UnsignCharacter(g.title.ToLower().Trim())
                                .Contains(HelperString.UnsignCharacter(title.ToLower().Trim())));
            }
            lstGoldPromo = lstGoldPromo.OrderByDescending(g => g.created);
            var lstCategory = _categoryRepository.GetWhere(g => g.type == (int)Config.TypeCategory.UuDai).OrderBy(g => g.weight).ToList();
            ViewBag.ListCategory = lstCategory;
            ViewBag.TieuDe = title;
            ViewBag.status = status;
            ViewBag.cat_id = cat_id;
            ViewBag.fromdate = fromdate;
            ViewBag.todate = todate;
            return View(lstGoldPromo.ToPagedList(page, 20));
        }

        public ActionResult Add()
        {
            var lstCategory = _categoryRepository.GetWhere(g => g.type == (int)Config.TypeCategory.UuDai).OrderBy(g => g.weight).ToList();
            ViewBag.ListCategory = lstCategory;
            var lstCatCard = _categoryRepository.GetWhere(g => g.type == (int)Config.TypeCategory.LoaiThe && g.parent_id == 0 && g.status == 1 && g.isPromotion == true).OrderBy(g => g.weight).ToList();
            ViewBag.ListCatCard = lstCatCard;
            var lstCity = _provinceRepository.GetAll().OrderBy(g => g.title).ToList();
            ViewBag.ListCity = lstCity;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(bidv__promotion item, string time_from, string time_to, int[] card, int[] city, string location, string sort, string body, HttpPostedFileBase filedoc, HttpPostedFileBase filengang, HttpPostedFileBase[] filephu)
        {
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            if (string.IsNullOrEmpty(item.title) || item.cat_id == 0 || item.promo_per == null || time_from == null || time_to == null || string.IsNullOrEmpty(location) || string.IsNullOrEmpty(sort) || string.IsNullOrEmpty(body))
            {
                return RedirectToAction("Add", "AdminGoldPromo");
            }
            item.status = 1;
            item.created = (int)HelperDateTime.Convert2TimeStamp(now);
            item.time_from = (int)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(time_from));
            item.time_to = (int)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(time_to));
            item.lang = "vi";
            item.pid = 0;
            item.view = 0;
            item.title_seo = HelperString.UnsignCharacter(item.title).Replace(" ", "-");
            var imagedoc = WebImage.GetImageFromRequest("filedoc");
            if (imagedoc != null)
            {
                var name = filedoc.FileName.Split('.')[0];
                var ext = filedoc.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Replace(' ','_'), timestamp, ext);
                var path1 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/{1}/{2}/size286", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                    now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                HelperImages.SaveAndResize(imagedoc, 245, 286, filename, path1);

                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/{1}/{2}/size80", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                    now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                HelperImages.SaveAndResizeImage(imagedoc, 80, filename, path);
                item.image_doc = filename;
            }
            var imagengang = WebImage.GetImageFromRequest("filengang");
            if (imagengang != null)
            {
                var name = filengang.FileName.Split('.')[0];
                var ext = filengang.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Replace(' ','_'), timestamp, ext);
                var path1 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/{1}/{2}/size670", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                    now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                HelperImages.SaveAndResize(imagengang, 670,440, filename, path1);
                var path2 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/{1}/{2}/size222", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                   now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                HelperImages.SaveAndResizeImage(imagengang, 222, filename, path2);
                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/{1}/{2}/size80", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                    now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                HelperImages.SaveAndResizeImage(imagengang, 80, filename, path);
                item.image_ngang = filename;
            }
            _promotionRepository.Add(item);
            if (card != null && card.Length > 0)
            {
                foreach (var cardid in card)
                {
                    var objPromoCard = new bidv__promotion_card { promo_id = item.id, card_id = cardid };
                    _promoCardRepository.Add(objPromoCard);
                }
            }
            if (city != null && city.Length > 0)
            {
                foreach (var cityid in city)
                {
                    var objPromoCity = new bidv__promotion_city { promo_id = item.id, city_id = cityid };
                    _promoCardCityRepository.Add(objPromoCity);
                }
            }
            
            foreach (var imgphu in filephu)
            {
                if (imgphu == null)
                {
                    continue;
                }
                var name = imgphu.FileName.Split('.')[0];
                var ext = imgphu.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Replace(' ','_'), timestamp, ext);
                var image = new WebImage(imgphu.InputStream);
                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/{1}/{2}/size80/", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                    now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                var promoimg = new bidv__promotion_img();
                promoimg.image = filename;
                HelperImages.SaveAndResizeImage(image, 80, filename, path);

                promoimg.promo_id = item.id;
                promoimg.weight = 0;
                promoimg.created = (int?)HelperDateTime.Convert2TimeStamp(now);
                promoimg.type = 0;
                _promoImageRepository.Add(promoimg);
            }
            var bidvpromoDes = new bidv__promotion_des
            {
                sort = sort,
                body = body,
                location = location,
                promo_id = item.id
            };
            _promoDesRepository.Add(bidvpromoDes);
            return RedirectToAction("Index", "AdminGoldPromo");
        }

        public ActionResult Edit(int id)
        {
            var lstCategory = _categoryRepository.GetWhere(g => g.type == (int)Config.TypeCategory.UuDai).OrderBy(g => g.weight).ToList();
            ViewBag.ListCategory = lstCategory;
            var lstCatCard = _categoryRepository.GetWhere(g => g.type == (int)Config.TypeCategory.LoaiThe && g.parent_id == 0 && g.status == 1 && g.isPromotion == true).OrderBy(g => g.weight).ToList();
            ViewBag.ListCatCard = lstCatCard;
            var lstCity = _provinceRepository.GetAll().OrderBy(g => g.title).ToList();
            ViewBag.ListCity = lstCity;
            var obj = _promotionRepository.GetById(id);
            return View(obj);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(bidv__promotion item, string time_from, string time_to, int[] card, int[] city, string location, string sort, string body, HttpPostedFileBase filedoc, HttpPostedFileBase filengang, HttpPostedFileBase[] filephu)
        {
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            if (string.IsNullOrEmpty(item.title) || item.cat_id == 0 || item.promo_per == null || time_from == null || time_to == null || string.IsNullOrEmpty(location) || string.IsNullOrEmpty(sort) || string.IsNullOrEmpty(body))
            {
                return RedirectToAction("Edit", "AdminGoldPromo", new {id = item.id});
            }
            var olditem = _promotionRepository.GetById(item.id);
            olditem.title = item.title;
            olditem.time_from = (int)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(time_from));
            olditem.time_to = (int)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(time_to));
            olditem.title_seo = HelperString.UnsignCharacter(olditem.title).Replace(" ", "-");
            olditem.cat_id = item.cat_id;
            olditem.promo_per = item.promo_per;
            olditem.weight = item.weight;
            olditem.company = item.company;
            olditem.website = item.website;
            olditem.is_home = item.is_home;
            olditem.is_hot = item.is_hot;
            olditem.created = (int)HelperDateTime.Convert2TimeStamp(now);
            var imagedoc = WebImage.GetImageFromRequest("filedoc");
            if (imagedoc != null)
            {
                var name = filedoc.FileName.Split('.')[0];
                var ext = filedoc.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Replace(' ','_'), timestamp, ext);
                var path1 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/{1}/{2}/size286", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                    now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                HelperImages.SaveAndResize(imagedoc, 245, 286, filename, path1);

                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/{1}/{2}/size80", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                    now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                HelperImages.SaveAndResizeImage(imagedoc, 80, filename, path);
                //if (!string.IsNullOrEmpty(olditem.image_doc))
                //{
                //    var oldDate = HelperDateTime.ConvertTimespan2DateTime(olditem.created).ToString("yyyy/MM/dd");
                //    var pathOldFile = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/size286/", oldDate));
                //    if (System.IO.File.Exists(pathOldFile + olditem.image_doc))
                //    {
                //        System.IO.File.Delete(pathOldFile + olditem.image_doc);
                //    }
                //    var pathOldFile1 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/size80/", oldDate));
                //    if (System.IO.File.Exists(pathOldFile1 + olditem.image_doc))
                //    {
                //        System.IO.File.Delete(pathOldFile1 + olditem.image_doc);
                //    }
                //}
                olditem.image_doc = filename;
            }
            var imagengang = WebImage.GetImageFromRequest("filengang");
            if (imagengang != null)
            {
                var name = filengang.FileName.Split('.')[0];
                var ext = filengang.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Replace(' ','_'), timestamp, ext);               
                var path2 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/{1}/{2}/size222", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                   now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                HelperImages.SaveAndResizeImage(imagengang, 222, filename, path2);

                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/{1}/{2}/size80", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                  now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                HelperImages.SaveAndResizeImage(imagengang, 80, filename, path);

                //if (!string.IsNullOrEmpty(olditem.image_ngang))
                //{
                //    var oldDate = HelperDateTime.ConvertTimespan2DateTime(olditem.created).ToString("yyyy/MM/dd");
                //    var pathOldFile = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/size222/", oldDate));
                //    if (System.IO.File.Exists(pathOldFile + olditem.image_ngang))
                //    {
                //        System.IO.File.Delete(pathOldFile + olditem.image_ngang);
                //    }
                //    var pathOldFile1 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/size80/", oldDate));
                //    if (System.IO.File.Exists(pathOldFile1 + olditem.image_ngang))
                //    {
                //        System.IO.File.Delete(pathOldFile1 + olditem.image_ngang);
                //    }
                //}
                olditem.image_ngang = filename;
            }
            _promotionRepository.Update(olditem);
            var lstCard = _promoCardRepository.GetWhere(g => g.promo_id == olditem.id).ToList();
            foreach (var bidvPromotionCard in lstCard)
            {
                _promoCardRepository.Delete(bidvPromotionCard);
            }
            if (card != null && card.Length > 0)
            {
                foreach (var cardid in card)
                {
                    var objPromoCard = new bidv__promotion_card { promo_id = olditem.id, card_id = cardid };
                    _promoCardRepository.Add(objPromoCard);
                }
            }
            
            var lstCity = _promoCardCityRepository.GetWhere(g => g.promo_id == olditem.id).ToList();
            foreach (var bidvPromotionCity in lstCity)
            {
                _promoCardCityRepository.Delete(bidvPromotionCity);
            }
            if (city != null && city.Length > 0)
            {
                foreach (var cityid in city)
                {
                    var objPromoCity = new bidv__promotion_city { promo_id = olditem.id, city_id = cityid };
                    _promoCardCityRepository.Add(objPromoCity);
                }
            }
            
            foreach (var imgphu in filephu)
            {
                if (imgphu == null) continue;
                var name = imgphu.FileName.Split('.')[0];
                var ext = imgphu.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Replace(' ','_'), timestamp, ext);
                var image = new WebImage(imgphu.InputStream);
                var path1 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/{1}/{2}/size670", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                   now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                HelperImages.SaveAndResize(image, 670, 440, filename, path1);

                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/{1}/{2}/size80/", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                    now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                HelperImages.SaveAndResizeImage(image, 80, filename, path);
                var lstimg = _promoImageRepository.GetWhere(a => a.promo_id == item.id).ToList();
                foreach (var bidvPromotionImg in lstimg)
                {
                    _promoImageRepository.Delete(bidvPromotionImg);
                }
                //if (promoimg != null)
                //{
                    //if (!string.IsNullOrEmpty(promoimg.image))
                    //{
                    //    if (promoimg.created != null)
                    //    {
                    //        var oldDate = HelperDateTime.ConvertTimespan2DateTime(promoimg.created.Value).ToString("yyyy/MM/dd");
                    //        var pathOldFile = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/size670/", oldDate));
                    //        if (System.IO.File.Exists(pathOldFile + promoimg.image))
                    //        {
                    //            System.IO.File.Delete(pathOldFile + promoimg.image);
                    //        }

                    //        var pathOldFile1 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/promotion/{0}/size80/", oldDate));
                    //        if (System.IO.File.Exists(pathOldFile1 + promoimg.image))
                    //        {
                    //            System.IO.File.Delete(pathOldFile1 + promoimg.image);
                    //        }
                    //    }
                    //}
                    var promoimg = new bidv__promotion_img();
                    promoimg.image = filename;
                    promoimg.promo_id = olditem.id;
                    promoimg.weight = 0;
                    promoimg.created = (int?)HelperDateTime.Convert2TimeStamp(now);
                    promoimg.type = 0;
                    _promoImageRepository.Add(promoimg);
                //}
            }
            var bidvpromoDes = _promoDesRepository.GetWhere(g => g.promo_id == item.id).FirstOrDefault();
            if (bidvpromoDes != null)
            {
                bidvpromoDes.sort = sort;
                bidvpromoDes.body = body;
                bidvpromoDes.location = location;
            }
            _promoDesRepository.Update(bidvpromoDes);
            return RedirectToAction("Index", "AdminGoldPromo");
        }
        public ActionResult Delete(int id)
        {
            var obj = _promotionRepository.GetById(id);
            obj.status = -1;
            _promotionRepository.Update(obj);
            return RedirectToAction("Index", "AdminGoldPromo");
        }

        public ActionResult RemovePromoImage(int id)
        {
            var obj = _promoImageRepository.GetById(id);
            _promoImageRepository.Delete(obj);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
