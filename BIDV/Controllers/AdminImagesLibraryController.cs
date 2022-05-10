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

namespace BIDV.Controllers
{
    [Authorize]
    public class AdminImagesLibraryController : BaseController
    {
        //
        // GET: /AdminImagesLibrary/
        readonly GalleryCatRepository _galleryCatRepository = new GalleryCatRepository();
        readonly GalleryRepository _galleryRepository = new GalleryRepository();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult _FormCategory()
        {
            var lstGalleryCat = _galleryCatRepository.GetAll().OrderBy(g => g.id).ToList();
            return Json(RenderViewToString("~/Views/AdminImagesLibrary/_FormCategory.cshtml", lstGalleryCat), JsonRequestBehavior.AllowGet);
        }

        public ActionResult _ListData(int id, int page = 1)
        {
            const int rowlitmit = 20;
            var objCat = _galleryCatRepository.GetById(id);
            TempData["CatName"] = objCat.title;
            var lstImages = _galleryRepository.GetAll().Where(g => g.cat_id == id);
            var totalNews = lstImages.Count();
            lstImages = lstImages.OrderByDescending(g => g.created).Skip((page - 1) * rowlitmit).Take(rowlitmit);
            return Json(new
            {
                viewContent = RenderViewToString("~/Views/AdminImagesLibrary/_ListData.cshtml", lstImages),
                totalPages = Math.Ceiling(((double)totalNews / rowlitmit)),
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _View(int id)
        {
            var objGallery = _galleryRepository.GetById(id);
            return Json(RenderViewToString("~/Views/AdminImagesLibrary/_View.cshtml", objGallery), JsonRequestBehavior.AllowGet);
        }

        public void Delete(int id)
        {
            var obj = _galleryRepository.GetById(id);
            _galleryRepository.Delete(obj);
            //return RedirectToAction("_ListData", "AdminImagesLibrary", new { id = cat_id });
        }
        public ActionResult _Edit(int id)
        {
            var obj = _galleryRepository.GetById(id);
            var lstGalleryCat = _galleryCatRepository.GetAll().OrderBy(g => g.id).ToList();
            TempData["ListGalleryCat"] = lstGalleryCat;
            return Json(RenderViewToString("~/Views/AdminImagesLibrary/_Edit.cshtml", obj), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void _Edit(bidv__gallery item)
        {
            var obj = _galleryRepository.GetById(item.id);
            obj.cat_id = item.cat_id;
            obj.image = item.image;
            obj.type = item.type;
            obj.created = item.created;
            _galleryRepository.Update(obj);
            //return RedirectToAction("_ListData", "AdminImagesLibrary", new {id = obj.cat_id});
        }
        [HttpPost]
        public ActionResult _EditUpload(HttpPostedFileBase fileData, int id)
        {
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            var image = new WebImage(fileData.InputStream);
            var filename = "";
            var type = "";
            if (image != null)
            {
                var name = fileData.FileName.Split('.')[0];
                var ext = fileData.FileName.Split('.')[1];
                filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Trim(), timestamp, ext);
                type = fileData.ContentType;
                //XFull
                var pathOriginal = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/gallery/{0}/{1}/{2}/origin/", now.Year, now.Month, now.Day));
                HelperImages.SaveImage(image, filename, pathOriginal);
                //X640
                var path640 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/gallery/{0}/{1}/{2}/size640/", now.Year, now.Month, now.Day));
                HelperImages.SaveAndResizeImage(image, 640, filename, path640);
                //X580
                var path580 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/gallery/{0}/{1}/{2}/size580/", now.Year, now.Month, now.Day));
                HelperImages.SaveAndResizeImage(image, 580, filename, path580);
                //X350
                var path350 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/gallery/{0}/{1}/{2}/size350/", now.Year, now.Month, now.Day));
                HelperImages.SaveAndResizeImage(image, 350, filename, path350);
                //X150
                var path150 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/gallery/{0}/{1}/{2}/size150/", now.Year, now.Month, now.Day));
                HelperImages.SaveAndResizeImage(image, 150, filename, path150);
            }
            return Json(new { filename = filename, type = type, created = timestamp }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void UploadImageLibrary(HttpPostedFileBase fileData, int catid)
        {
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            var image = new WebImage(fileData.InputStream);
            var filename = "";
            var type = "";
            if (image != null)
            {
                var name = fileData.FileName.Split('.')[0];
                var ext = fileData.FileName.Split('.')[1];
                filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Trim(), timestamp, ext);
                type = fileData.ContentType;
                //XFull
                var pathOriginal = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/gallery/{0}/{1}/{2}/origin/", now.Year, now.Month, now.Day));
                HelperImages.SaveImage(image, filename, pathOriginal);
                //X640
                var path640 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/gallery/{0}/{1}/{2}/size640/", now.Year, now.Month, now.Day));
                HelperImages.SaveAndResizeImage(image, 640, filename, path640);
                //X580
                var path580 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/gallery/{0}/{1}/{2}/size580/", now.Year, now.Month, now.Day));
                HelperImages.SaveAndResizeImage(image, 580, filename, path580);
                //X350
                var path350 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/gallery/{0}/{1}/{2}/size350/", now.Year, now.Month, now.Day));
                HelperImages.SaveAndResizeImage(image, 350, filename, path350);
                //X150
                var path150 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/gallery/{0}/{1}/{2}/size150/", now.Year, now.Month, now.Day));
                HelperImages.SaveAndResizeImage(image, 150, filename, path150);
                var objGallery = new bidv__gallery();
                objGallery.cat_id = catid;
                objGallery.title = name;
                objGallery.image = filename;
                objGallery.changed = 0;
                objGallery.created = (int)timestamp;
                objGallery.type = type;
                objGallery.uid = User.ID;
                objGallery.uname = User.UserName;
                _galleryRepository.Add(objGallery);
            }
            //return RedirectToAction("_ListData", "AdminImagesLibrary", new { id = catid });
        }

        public ActionResult _AddCategory()
        {
            return Json(RenderViewToString("~/Views/AdminImagesLibrary/_AddCategory.cshtml"), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult _AddCategory(bidv__gallery_cats item)
        {
            if (string.IsNullOrEmpty(item.title) || string.IsNullOrEmpty(item.description))
            {
                return RedirectToAction("_AddCategory", "AdminImagesLibrary");
            }
            item.created = (int)HelperDateTime.Convert2TimeStamp(DateTime.Now);
            item.total = 0;
            item.uid = User.ID;
            item.uname = User.UserName;
            _galleryCatRepository.Add(item);
            var lstGalleryCat = _galleryCatRepository.GetAll().OrderBy(g => g.id).ToList();
            return Json(RenderViewToString("~/Views/AdminImagesLibrary/_FormCategory.cshtml", lstGalleryCat), JsonRequestBehavior.AllowGet);
        }

        public ActionResult _EditCategory(int id)
        {
            var obj = _galleryCatRepository.GetById(id);
            return Json(RenderViewToString("~/Views/AdminImagesLibrary/_EditCategory.cshtml", obj), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult _EditCategory(bidv__gallery_cats item)
        {
            if (string.IsNullOrEmpty(item.title) || string.IsNullOrEmpty(item.description))
            {
                return RedirectToAction("_AddCategory", "AdminImagesLibrary");
            }
            var oldObj = _galleryCatRepository.GetById(item.id);
            oldObj.title = item.title;
            oldObj.description = item.description;
            _galleryCatRepository.Update(oldObj);
            var lstGalleryCat = _galleryCatRepository.GetAll().OrderBy(g => g.id).ToList();
            return Json(RenderViewToString("~/Views/AdminImagesLibrary/_FormCategory.cshtml", lstGalleryCat), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCategory(int id)
        {
            var obj = _galleryCatRepository.GetById(id);
            try
            {
                _galleryCatRepository.Delete(obj);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
