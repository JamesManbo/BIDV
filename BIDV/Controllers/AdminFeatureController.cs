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
    public class AdminFeatureController : BaseController
    {
        readonly FeatureRepository _featureRepository = new FeatureRepository();
        //
        // GET: /AdminFeature/

        public ActionResult Index(string title, string url_show, int page = 1)
        {
            ViewBag.tieude = title;
            ViewBag.url_show = url_show;
            var listBannerPopup = _featureRepository.GetWhere(g=>g.type == 0);
            if (!string.IsNullOrEmpty(title))
            {
                ViewBag.TitleContent = title;
                listBannerPopup = listBannerPopup.Where(g => !string.IsNullOrEmpty(g.title) && HelperString.UnsignCharacter(g.title.ToLower().Trim())
                                .Contains(HelperString.UnsignCharacter(title.ToLower().Trim()))); ;
            }

            if (!string.IsNullOrEmpty(url_show))
            {
                ViewBag.URL = url_show;
                listBannerPopup = listBannerPopup.Where(a => a.url_show == url_show);
            }
            listBannerPopup = listBannerPopup.OrderByDescending(g => g.created);
            return View(listBannerPopup.ToPagedList(page, Config.PageSize));
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(BIDV.Model.bidv__feature bidvFeature, string start, string end, HttpPostedFileBase file)
        {
            if (string.IsNullOrEmpty(bidvFeature.title) || string.IsNullOrEmpty(bidvFeature.link) || string.IsNullOrEmpty(bidvFeature.url_show))
            {
                return RedirectToAction("Add", "AdminFeature");
            }
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            bidvFeature.type = 1;
            bidvFeature.created = (int)HelperDateTime.Convert2TimeStamp(now);
            bidvFeature.start = (int?)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(start));
            bidvFeature.end = (int?)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(end));
            var image = WebImage.GetImageFromRequest("file");
            if (image != null)
            {
                var name = file.FileName.Split('.')[0];
                var ext = file.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Trim(), timestamp, ext);
                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/feature/{0}/{1}/{2}/size1300/", now.Year, now.Month, now.Day));
                bidvFeature.image = filename;
                bidvFeature.created = (int?)HelperDateTime.Convert2TimeStamp(now);
                HelperImages.SaveAndResizeImage(image, 1300, filename, path);

                var path2 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/feature/{0}/{1}/{2}/size215/", now.Year, now.Month, now.Day));
                HelperImages.SaveAndResizeImage(image, 215, filename, path2);

            }
            bidvFeature.type = 0;//Popup
            _featureRepository.Add(bidvFeature);
            return RedirectToAction("Index", "AdminFeature");
        }

        public ActionResult Edit(int id)
        {
            var item = _featureRepository.GetById(id);
            if (item == null)
            {
                return HttpNotFound();

            }
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(BIDV.Model.bidv__feature bidvFeature, string start, string end, HttpPostedFileBase file)
        {
            if (string.IsNullOrEmpty(bidvFeature.title) || string.IsNullOrEmpty(bidvFeature.link) || string.IsNullOrEmpty(bidvFeature.url_show))
            {
                return RedirectToAction("Edit", "AdminFeature", new { id = bidvFeature.id });
            }
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            var oldItem = _featureRepository.GetById(bidvFeature.id);
            oldItem.title = bidvFeature.title;
            oldItem.link = bidvFeature.link;
            oldItem.url_show = bidvFeature.url_show;
            oldItem.body = bidvFeature.body;
            var image = WebImage.GetImageFromRequest("file");
            //if (!string.IsNullOrEmpty(oldItem.image) && image != null)
            //{
            //    if (oldItem.created != null)
            //    {
            //        //var oldDate = HelperDateTime.ConvertTimespan2DateTime(oldItem.created.Value);
            //        var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/feature/{0}/size1300/",now.ToString("yyyy/MM/dd")));
            //        if (System.IO.File.Exists(path))
            //        {
            //            System.IO.File.Delete(path);
            //        }
            //        var path2 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/feature/{0}/size215/", now.ToString("yyyy/MM/dd")));
            //        if (System.IO.File.Exists(path2))
            //        {
            //            System.IO.File.Delete(path2);
            //        }
            //    }
            //}
            oldItem.status = 1;
            oldItem.created = (int?) timestamp;
            if (!string.IsNullOrEmpty(start))
            {
                oldItem.start = (int?)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(start));
            }
            if (!string.IsNullOrEmpty(end))
            {
                oldItem.end = (int?)HelperDateTime.Convert2TimeStamp(Convert.ToDateTime(end));
            }

            //if (image != null)
            {
                var name = file.FileName.Split('.')[0];
                var ext = file.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Trim(), timestamp, ext);
                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/feature/{0}/size1300/", now.ToString("yyyy/MM/dd")));
                oldItem.image = filename;
                HelperImages.SaveAndResizeImage(image, 1300, filename, path);

                var path2 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/feature/{0}/size215/",now.ToString("yyyy/MM/dd")));
                HelperImages.SaveAndResizeImage(image, 215, filename, path2);
            }
            oldItem.type = 0;//Popup
            _featureRepository.Update(oldItem);
            return RedirectToAction("Index", "AdminFeature");
        }
        public ActionResult Delete(int id)
        {
            bidv__feature bidvFeature = _featureRepository.GetById(id);
            if (bidvFeature == null)
            {
                return HttpNotFound();
            }
            _featureRepository.Delete(bidvFeature);
            return RedirectToAction("Index");

        }
        public ActionResult DeleteAll(string listId)
        {
            var arrId = listId.Split(',');
            foreach (var id in arrId)
            {

                bidv__feature bidvFeature = _featureRepository.GetById(Int32.Parse(id));
                if (bidvFeature == null)
                {
                    return HttpNotFound();
                }
                _featureRepository.Delete(bidvFeature);
            }

            return RedirectToAction("Index");

        }
    }
}
