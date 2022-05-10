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
    public class AdminSlideController : Controller
    {
        //
        // GET: /AdminSlide/
        readonly FeatureRepository _featureRepository = new FeatureRepository();
        public ActionResult Index(string url_show, int page = 1)
        {
            var lstFeatureImage = _featureRepository.GetAll().Where(g => g.status == 1 && g.type == 1);
            if (!string.IsNullOrEmpty(url_show))
            {
                lstFeatureImage = lstFeatureImage.Where(g => g.url_show == url_show);
            }
            lstFeatureImage = lstFeatureImage.OrderByDescending(g => g.created);
            ViewBag.url_show = url_show;
            return View(lstFeatureImage.ToPagedList(page, Config.PageSize));
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(bidv__feature item, HttpPostedFileBase file)
        {
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            var image = WebImage.GetImageFromRequest("file");
            if (string.IsNullOrEmpty(item.title) || string.IsNullOrEmpty(item.link) || string.IsNullOrEmpty(item.url_show))
            {
                return RedirectToAction("Add","AdminSlide");
            }
            if (image != null)
            {
                var name = file.FileName.Split('.')[0];
                var ext = file.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Trim(), timestamp, ext);
                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/feature/{0}/{1}/{2}/size1300", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                    now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                item.image = filename;
                HelperImages.SaveAndResizeImage(image, 1300, filename, path);

                var path2 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/feature/{0}/{1}/{2}/size215/", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                   now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                HelperImages.SaveAndResizeImage(image, 215, filename, path2);

                item.created = (int)HelperDateTime.Convert2TimeStamp(now);
            }
            item.status = 1;
            item.show = 1;
            item.type = 1;//Slide
            _featureRepository.Add(item);
            return RedirectToAction("Index", "AdminSlide");
        }

        public ActionResult Edit(int id)
        {
            var objSlide = _featureRepository.GetById(id);
            return View(objSlide);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(bidv__feature item, HttpPostedFileBase file)
        {
            if (string.IsNullOrEmpty(item.title) || string.IsNullOrEmpty(item.link) || string.IsNullOrEmpty(item.url_show))
            {
                return RedirectToAction("Edit", "AdminSlide", new { id = item.id });
            }
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            var image = WebImage.GetImageFromRequest("file");
            if (!string.IsNullOrEmpty(item.image) && image != null)
            {
                if (item.created != null)
                {
                    var oldDate = HelperDateTime.ConvertTimespan2DateTime(item.created.Value);
                    var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/feature/{0}/{1}/{2}/size1300/", oldDate.Year, oldDate.Month, oldDate.Day));
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    var path2 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/feature/{0}/{1}/{2}/size215/", oldDate.Year, oldDate.Month, oldDate.Day));
                    if (System.IO.File.Exists(path2))
                    {
                        System.IO.File.Delete(path2);
                    }
                }
            }
            if (image != null)
            {
                var name = file.FileName.Split('.')[0];
                var ext = file.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Trim(), timestamp, ext);
                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/feature/{0}/{1}/{2}/size1300", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                    now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                    item.image = filename;
                    HelperImages.SaveAndResizeImage(image, 1300, filename, path);

                var path2 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/feature/{0}/{1}/{2}/size215/", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                    now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                HelperImages.SaveAndResizeImage(image, 215, filename, path2);

                item.created = (int)HelperDateTime.Convert2TimeStamp(now);
            }
            item.type = 1;//Slide
            _featureRepository.Update(item);
            return RedirectToAction("Index", "AdminSlide");
        }

        public ActionResult Delete(int id)
        {
            var objFeature = _featureRepository.GetById(id);
            objFeature.status = -1;
            _featureRepository.Update(objFeature);
            return RedirectToAction("Index", "AdminSlide");
        }

        public ActionResult Show(int id, int show)
        {
            var objFeature = _featureRepository.GetById(id);
            objFeature.show = (short?)show;
            _featureRepository.Update(objFeature);
            return Json(show, JsonRequestBehavior.AllowGet);
        }
    }
}
