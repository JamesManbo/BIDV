using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
    public class AdminNewsController : BaseController
    {
        //
        // GET: /AdminNews/
        readonly CategoryRepository _categoryRepository = new CategoryRepository();
        readonly NewsRepository _newsRepository = new NewsRepository();
        readonly NewsImageRepository _newsImageRepository = new NewsImageRepository();
        readonly TagRepository _tagRepository = new TagRepository();
        readonly TagItemsRepository _tagItemsRepository = new TagItemsRepository();
        public ActionResult Index(string title, int? status, int? cat_id, DateTime? fromdate, DateTime? todate, int page = 1)
        {
            var lstCategory = _categoryRepository.GetWhere(g => g.type == (int)Config.TypeCategory.TinTuc);
            ViewBag.ListCategory = lstCategory.OrderByDescending(g => g.created).ToList();
            var lstNews = _newsRepository.GetWhere(g => g.bidv__category.type == (int)Config.TypeCategory.TinTuc && g.status != -1);
            if (cat_id != null && cat_id > 0)
            {
                lstNews = lstNews.Where(g => g.cat_id == cat_id.Value);

            }
            if (fromdate != null)
            {
                lstNews = lstNews.Where(g => HelperDateTime.Convert2TimeStamp(fromdate.Value) <= g.created);
            }
            if (todate != null)
            {
                lstNews = lstNews.Where(g => HelperDateTime.Convert2TimeStamp(todate.Value) >= g.created);
            }
            if (status != null && status != -2)
            {
                lstNews = lstNews.Where(g => g.status == status.Value);
            }
            else
            {
                lstNews = lstNews.Where(g => g.status != -1);
            }
            if (!string.IsNullOrEmpty(title))
            {
                lstNews =
                    lstNews.Where(
                        g =>
                            HelperString.UnsignCharacter(g.title.ToLower().Trim())
                                .Contains(HelperString.UnsignCharacter(title.ToLower().Trim())));
            }
            ViewBag.TieuDe = title;
            ViewBag.status = status;
            ViewBag.cat_id = cat_id;
            ViewBag.fromdate = fromdate;
            ViewBag.todate = todate;

            lstNews = lstNews.OrderByDescending(g => g.created);
            return View(lstNews.ToPagedList(page, Config.PageSize));
        }

        public ActionResult Add()
        {
            var lstCats = _categoryRepository.GetWhere(g => g.type == (int)Config.TypeCategory.TinTuc);
            ViewBag.ListCategory = lstCats.OrderBy(g => g.weight).ToList();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(bidv__news item, string dateof, HttpPostedFileBase file)
        {
            if (string.IsNullOrEmpty(item.title) || string.IsNullOrEmpty(dateof) || item.cat_id == null ||
                string.IsNullOrEmpty(item.sort_body) || string.IsNullOrEmpty(item.body))
            {
                return RedirectToAction("Add", "AdminNews");
            }
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            item.type = 0;
            item.created = (int)HelperDateTime.Convert2TimeStamp(now);
            item.uid = User.ID;
            item.user_name = User.UserName;
            item.dateof = (int?)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(dateof));
            _newsRepository.Add(item);
            var image = WebImage.GetImageFromRequest("file");
            if (image != null)
            {
                var name = file.FileName.Split('.')[0];
                var ext = file.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Replace(' ', '_'), timestamp, ext);
                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/news/{0}/size306/", now.ToString("yyyy/MM/dd")));
                var objNewsImages = new bidv__news_image();
                objNewsImages.image = filename;
                objNewsImages.news_id = item.id;
                objNewsImages.created = (int?)HelperDateTime.Convert2TimeStamp(now);
                objNewsImages.type = 0;
                HelperImages.SaveAndResizeImage(image, 306, filename, path);
                _newsImageRepository.Add(objNewsImages);
            }

            return RedirectToAction("Index", "AdminNews");
        }

        public ActionResult Edit(int id)
        {
            var obj = _newsRepository.GetById(id);
            var lstCats = _categoryRepository.GetWhere(g => g.type == (int)Config.TypeCategory.TinTuc);
            ViewBag.ListCategory = lstCats.OrderBy(g => g.weight).ToList();
            return View(obj);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(bidv__news item, string dateof, HttpPostedFileBase file)
        {
            if (string.IsNullOrEmpty(item.title) || string.IsNullOrEmpty(dateof) || item.cat_id == null ||
              string.IsNullOrEmpty(item.sort_body) || string.IsNullOrEmpty(item.body))
            {
                return RedirectToAction("Edit", "AdminNews", new { id = item.id });
            }
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            item.changed = (int)HelperDateTime.Convert2TimeStamp(now);
            item.dateof = (int)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(dateof));
            _newsRepository.Update(item);
            var image = WebImage.GetImageFromRequest("file");
            if (image != null)
            {
                var name = file.FileName.Split('.')[0];
                var ext = file.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Replace(' ', '_'), timestamp, ext);
                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/news/{0}/size306/", now.ToString("yyyy/MM/dd")));
                var lstNewsImages = _newsImageRepository.GetWhere(a => a.news_id == item.id).ToList();
                foreach (var itemimgs in lstNewsImages)
                {
                    _newsImageRepository.Delete(itemimgs);
                }
                var objNewsImages = new bidv__news_image();
                objNewsImages.image = filename;
                objNewsImages.news_id = item.id;
                objNewsImages.created = (int?)HelperDateTime.Convert2TimeStamp(now);
                objNewsImages.type = 0;
                HelperImages.SaveAndResizeImage(image, 306, filename, path);
                _newsImageRepository.Add(objNewsImages);
            }
            return RedirectToAction("Index", "AdminNews");
        }

        public ActionResult Delete(int id)
        {
            var objNews = _newsRepository.GetById(id);
            objNews.status = -1;
            _newsRepository.Update(objNews);
            return RedirectToAction("Index", "AdminNews");
        }

        public ActionResult DeleteTag(int id)
        {
            var objTag = _tagItemsRepository.GetAll().FirstOrDefault(a => a.id == id);
            if (objTag != null)
            {
                _tagItemsRepository.Delete(objTag);
            }
            return RedirectToAction("Index", "AdminNews");
        }
        public ActionResult UpdateStatus(int id)
        {
            var objNews = _newsRepository.GetById(id);
            if (objNews.status == 1)
            {
                objNews.status = 0;
            }
            else if (objNews.status == 0)
            {
                objNews.status = 1;
            }
            _newsRepository.Update(objNews);
            return Json(objNews.status, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddTagName(string tagName, int? newsId)
        {
            if (newsId == null) return RedirectToAction("Index", "AdminNews");
            var objTag = new bidv__tags
            {
                title = tagName,
                tag = HelperString.UnsignCharacter(tagName),
                created = (int?)HelperDateTime.Convert2TimeStamp(DateTime.Now),
                status = 1
            };
            var curTagId = _tagRepository.AddAndGetId(objTag);
            var objTagItems = new bidv__tag_items { tag_id = curTagId, item_id = newsId.Value, type = 0 };
            _tagItemsRepository.Add(objTagItems);
            return RedirectToAction("Index", "AdminNews");
        }
    }
}
