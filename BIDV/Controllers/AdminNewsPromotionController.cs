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
    public class AdminNewsPromotionController : BaseController
    {
        readonly NewsRepository _newsRepository = new NewsRepository();
        readonly NewsImageRepository _newsImageRepository = new NewsImageRepository();
        readonly CategoryRepository _categoryRepository = new CategoryRepository();
        readonly TagRepository _tagRepository = new TagRepository();
        readonly TagItemsRepository _tagItemsRepository = new TagItemsRepository();
        //
        // GET: /AdminPromotion/

        public ActionResult Index(int? id, string title, Int16? status, int? catId, string timeFrom, string timeTo, int page = 1)
        {
            var listNews = _newsRepository.GetAll().Where(a => a.cat_id == 12);

            if (id >= 0)
            {
                listNews = listNews.Where(g => g.id == id);
            }
            if (!string.IsNullOrEmpty(title))
            {
                listNews = listNews.Where(g => g.title.Contains(title) || g.body.Contains(title));
            }
            if (status != -2 && status != null)
            {
                listNews = listNews.Where(g => g.status == status);
            }
            else
            {
                listNews = listNews.Where(g => g.status != -1);
            }
            if (catId != null && catId > 0)
            {
                listNews = listNews.Where(g => g.cat_id == catId);
               
            }
            if (!string.IsNullOrEmpty(timeFrom) && !string.IsNullOrEmpty(timeTo))
            {
                var dateFrom = Convert.ToDateTime(timeFrom);
                var dateTo = Convert.ToDateTime(timeTo);
                if (dateFrom > DateTime.MinValue && dateTo > DateTime.MinValue)
                {
                    listNews = listNews.Where(g => g.created >= HelperDateTime.Convert2TimeStamp(dateFrom) && g.created <= HelperDateTime.Convert2TimeStamp(dateTo));
                }
            }
            var lstCats = _categoryRepository.GetWhere(g => g.type == (int)Config.TypeCategory.TinKhuyenMai);
            ViewBag.ListCategory = lstCats.OrderBy(g => g.weight).ToList();
            listNews = listNews.OrderByDescending(a => a.created);
            ViewBag.Id = id;
            ViewBag.TitleContent = title;
            ViewBag.Status = status;
            ViewBag.CatId = catId;
            ViewBag.TimeFrom = timeFrom;
            ViewBag.TimeTo = timeTo;
            return View(listNews.ToPagedList(page, Config.PageSize));
        }
        public ActionResult Add()
        {
            var listCats = _categoryRepository.GetWhere(a => a.type == (int)Config.TypeCategory.TinKhuyenMai);
            ViewBag.ListCategory = listCats.OrderBy(a => a.weight).ToList();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(bidv__news bidvNews, string dateof, string time_from, string time_to, HttpPostedFileBase file)
        {
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            if (string.IsNullOrEmpty(bidvNews.title) || string.IsNullOrEmpty(dateof) || string.IsNullOrEmpty(time_from) ||
                string.IsNullOrEmpty(time_to) || bidvNews.cat_id == null || string.IsNullOrEmpty(bidvNews.sort_body) ||
                string.IsNullOrEmpty(bidvNews.body))
            {
                return RedirectToAction("Add", "AdminNewsPromotion");
            }
            bidvNews.created = (int)HelperDateTime.Convert2TimeStamp(now);
            bidvNews.dateof = (int?)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(dateof));
            bidvNews.time_from = (int?)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(time_from));
            bidvNews.time_to = (int?)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(time_to));
            bidvNews.uid = User.ID;
            bidvNews.type = 3;
            bidvNews.user_name = User.UserName;
            _newsRepository.Add(bidvNews);
            var image = WebImage.GetImageFromRequest("file");
            if (image != null)
            {
                var name = file.FileName.Split('.')[0];
                var ext = file.FileName.Split('.')[1];
                var strDate = now.ToString("yyyy/MM/dd");
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Replace(' ','_'), timestamp, ext);
                var path1 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/news/{0}/size580/", strDate));
                HelperImages.SaveAndResizeImage(image, 580, filename, path1);

                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/news/{0}/size306/", strDate));
                HelperImages.SaveAndResizeImage(image, 306, filename, path);

                var path3 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/news/{0}/size286/", strDate));
                HelperImages.SaveAndResizeImage(image, 286, filename, path3);
                var objNewsImages = new bidv__news_image();
                objNewsImages.image = filename;
                objNewsImages.news_id = bidvNews.id;
                objNewsImages.created = (int?)HelperDateTime.Convert2TimeStamp(now);
                objNewsImages.type = 0;
                _newsImageRepository.Add(objNewsImages);
            }
            return RedirectToAction("Index", "AdminNewsPromotion");
        }

        public ActionResult Edit(int id)
        {
            var listNews = _newsRepository.GetById(id);
            if (listNews == null)
            {
                return HttpNotFound();

            }
            var lstCats = _categoryRepository.GetWhere(g => g.type == (int)Config.TypeCategory.TinKhuyenMai);
            ViewBag.ListCategory = lstCats.OrderBy(g => g.weight).ToList();
            return View(listNews);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(bidv__news bidvNews, string dateof, string time_from, string time_to, HttpPostedFileBase file)
        {
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            if (string.IsNullOrEmpty(bidvNews.title) || string.IsNullOrEmpty(dateof) || string.IsNullOrEmpty(time_from) ||
            string.IsNullOrEmpty(time_to) || bidvNews.cat_id == null || string.IsNullOrEmpty(bidvNews.sort_body) ||
            string.IsNullOrEmpty(bidvNews.body))
            {
                return RedirectToAction("Edit", "AdminNewsPromotion", new { id = bidvNews.id });
            }
            bidvNews.dateof = (int?)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(dateof));
            bidvNews.time_from = (int?)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(time_from));
            bidvNews.time_to = (int?)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(time_to));
            bidvNews.changed = (int)HelperDateTime.Convert2TimeStamp(now);
            _newsRepository.Update(bidvNews);
            var image = WebImage.GetImageFromRequest("file");
            if (image != null)
            {
                var name = file.FileName.Split('.')[0];
                var ext = file.FileName.Split('.')[1];
                var strDate = now.ToString("yyyy/MM/dd");
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Replace(' ','_'), timestamp, ext);
                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/news/{0}/size306/", strDate));
                HelperImages.SaveAndResizeImage(image, 306, filename, path);
                var path1 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/news/{0}/size580/", strDate));
                HelperImages.SaveAndResizeImage(image, 580, filename, path1);
                var path3 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/news/{0}/size286/", strDate));
                HelperImages.SaveAndResizeImage(image, 286, filename, path3);
                var lstNewsImages = _newsImageRepository.GetWhere(a => a.news_id == bidvNews.id).ToList();
                foreach (var bidvNewsImage in lstNewsImages)
                {
                    _newsImageRepository.Delete(bidvNewsImage);
                }
                //if (!string.IsNullOrEmpty(objNewsImages.image))
                //{
                //    if (objNewsImages.created != null)
                //    {
                //        var oldDate = HelperDateTime.ConvertTimespan2DateTime(objNewsImages.created.Value).ToString("yyyy/MM/dd");
                //        var pathOldFile = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/news/{0}/size306/", oldDate));
                //        if (System.IO.File.Exists(pathOldFile + objNewsImages.image))
                //        {
                //            System.IO.File.Delete(pathOldFile + objNewsImages.image);
                //        }

                //        var pathOldFile1 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/news/{0}/size580/", oldDate));
                //        if (System.IO.File.Exists(pathOldFile1 + objNewsImages.image))
                //        {
                //            System.IO.File.Delete(pathOldFile1 + objNewsImages.image);
                //        }

                //        var pathOldFile2 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/news/{0}/size286/", oldDate));
                //        if (System.IO.File.Exists(pathOldFile2 + objNewsImages.image))
                //        {
                //            System.IO.File.Delete(pathOldFile2 + objNewsImages.image);
                //        }
                //    }
                  
                //}
                var objNewsImages = new bidv__news_image();
                objNewsImages.image = filename;
                objNewsImages.news_id = bidvNews.id;
                objNewsImages.created = (int?)HelperDateTime.Convert2TimeStamp(now);
                objNewsImages.type = 0;
                _newsImageRepository.Add(objNewsImages);
            }

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            bidv__news bidvNews = _newsRepository.GetById(id);
            if (bidvNews == null)
            {
                return HttpNotFound();
            }
            bidvNews.status = -1; // Set trạng thái về -1 : đã xóa
            _newsRepository.Update(bidvNews);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAll(string listId)
        {
            var arrId = listId.Split(',');
            foreach (var id in arrId)
            {
                bidv__news bidvNews = _newsRepository.GetById(Int32.Parse(id));
                if (bidvNews == null)
                {
                    return HttpNotFound();
                }
                bidvNews.status = -1; // Set trạng thái về -1 : đã xóa
                _newsRepository.Update(bidvNews);
            }

            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult AddTagName(string tagName, int? newsId)
        {
            if (newsId != null)
            {
                var objTag = new bidv__tags();
                objTag.title = tagName;
                objTag.tag = HelperString.UnsignCharacter(tagName);
                objTag.created = (int?)HelperDateTime.Convert2TimeStamp(DateTime.Now);
                objTag.status = 1;
                var curTagId = _tagRepository.AddAndGetId(objTag);
                var objTagItems = new bidv__tag_items();
                objTagItems.tag_id = curTagId;
                objTagItems.item_id = newsId.Value;
                objTagItems.type = 0;
                _tagItemsRepository.Add(objTagItems);
            }
            return RedirectToAction("Index", "AdminNewsPromotion");
        }
    }
}
