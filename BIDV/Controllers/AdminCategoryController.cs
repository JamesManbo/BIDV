using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BIDV.BaseSecurity;
using BIDV.Common;
using BIDV.Model;
using BIDV.Repository;
using Microsoft.Ajax.Utilities;

namespace BIDV.Controllers
{
    [CustomAuthorize(Config.TypeUser.SuperAdmin, Config.TypeUser.Admin)]
    public class AdminCategoryController : BaseController
    {
        //
        // GET: /AdminCategory/
        readonly CategoryRepository _categoryRepository = new CategoryRepository();
        public ActionResult Index()
        {
            var lstCategory = _categoryRepository.GetWhere(g => g.status == 1);
            return View(lstCategory.ToList());
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(bidv__category item, HttpPostedFileBase fileicon, HttpPostedFileBase filebanner)
        {
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);

            var imageicon = WebImage.GetImageFromRequest("fileicon");
            if (string.IsNullOrEmpty(item.title) || item.type == null)
            {
                return RedirectToAction("Add", "AdminCategory");
            }
            if (imageicon != null)
            {
                var name = fileicon.FileName.Split('.')[0];
                var ext = fileicon.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", name, timestamp, ext);
                var path = Server.MapPath("~/Content/FrontEnd/_img_server/siteInfo");
                item.icon = filename;
                HelperImages.SaveAndResizeImage(imageicon, 120, filename, path);
            }
            var imagebanner = WebImage.GetImageFromRequest("filebanner");
            if (imagebanner != null)
            {
                var name = filebanner.FileName.Split('.')[0];
                var ext = filebanner.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", name, timestamp, ext);
                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/category/{0}/{1}/{2}/size1500", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                    now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                item.image = filename;
                HelperImages.SaveAndResizeImage(imagebanner, 1500, filename, path);
            }
            item.created = (int)HelperDateTime.Convert2TimeStamp(now);
            item.status = 1;
            item.diem_gd = string.IsNullOrEmpty(item.diem_gd) ? "" : item.diem_gd;
            item.mota = string.IsNullOrEmpty(item.mota) ? "" : item.mota;
            item.tinh_nang = string.IsNullOrEmpty(item.tinh_nang) ? "" : item.tinh_nang;
            item.sosanh = string.IsNullOrEmpty(item.sosanh) ? "" : item.sosanh;
            item.safe_title = HelperString.UnsignCharacter(item.title).Replace(' ', '-');
            item.active = 0;
            _categoryRepository.Add(item);
            return RedirectToAction("Index", "AdminCategory");
        }


        public ActionResult GetCategoryByTypeCat(int typeid)
        {
            var lstCategory = _categoryRepository.GetWhere(g => g.type == typeid && g.status == 1);
            return Json(RenderViewToString("~/Views/AdminCategory/_DropdownCategory.cshtml", lstCategory.ToList()), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadFormAddByTypeCat(int typeid)
        {
            switch (typeid)
            {
                case (int)Config.TypeCategory.TinTuc:
                case (int)Config.TypeCategory.UuDai:
                case (int)Config.TypeCategory.TinKhuyenMai:
                    return Json(RenderViewToString("~/Views/AdminCategory/_FormAddOtherNews.cshtml"), JsonRequestBehavior.AllowGet);
                default:
                    return Json(RenderViewToString("~/Views/AdminCategory/_FormAddOtherCard.cshtml"), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadFormEditByTypeCat(int typeid, int catid)
        {
            var objCategory = _categoryRepository.GetById(catid);
            switch (typeid)
            {
                case (int)Config.TypeCategory.TinTuc:
                case (int)Config.TypeCategory.UuDai:
                case (int)Config.TypeCategory.TinKhuyenMai:
                    return Json("", JsonRequestBehavior.AllowGet);
                default:
                    return Json(RenderViewToString("~/Views/AdminCategory/_FormEditOtherCard.cshtml", objCategory), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Edit(int id)
        {
            var objCategory = _categoryRepository.GetById(id);
            return View(objCategory);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(bidv__category item, HttpPostedFileBase fileicon, HttpPostedFileBase filebanner)
        {
            if (string.IsNullOrEmpty(item.title) || item.type == null)
            {
                return RedirectToAction("Edit", "AdminCategory", new { id = item.id });
            }
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            var imageicon = WebImage.GetImageFromRequest("fileicon");
            if (imageicon != null)
            {
                var name = fileicon.FileName.Split('.')[0];
                var ext = fileicon.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", name, timestamp, ext);
                var path = Server.MapPath("~/Content/FrontEnd/_img_server/siteInfo");
                item.icon = filename;
                HelperImages.SaveAndResizeImage(imageicon, 120, filename, path);
            }
            var imagebanner = WebImage.GetImageFromRequest("filebanner");
            if (imagebanner != null)
            {
                var name = filebanner.FileName.Split('.')[0];
                var ext = filebanner.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", name, timestamp, ext);
                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/category/{0}/{1}/{2}/size1500", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                    now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                item.image = filename;
                HelperImages.SaveAndResizeImage(imagebanner, 1500, filename, path);
            }
            if (imagebanner != null || imageicon != null)
            {
                item.created = (int)HelperDateTime.Convert2TimeStamp(now);
            }
            item.safe_title = HelperString.UnsignCharacter(item.title).Replace(' ', '-');
            item.diem_gd = string.IsNullOrEmpty(item.diem_gd) ? "" : item.diem_gd;
            item.mota = string.IsNullOrEmpty(item.mota) ? "" : item.mota;
            item.tinh_nang = string.IsNullOrEmpty(item.tinh_nang) ? "" : item.tinh_nang;
            item.sosanh = string.IsNullOrEmpty(item.sosanh) ? "" : item.sosanh;
            item.safe_title = HelperString.UnsignCharacter(item.title).Replace(' ', '-');
            _categoryRepository.Update(item);
            return RedirectToAction("Index", "AdminCategory");
        }

        public ActionResult Delete(int id)
        {
            var obj = _categoryRepository.GetById(id);
            obj.status = -1;
            _categoryRepository.Update(obj);
            return RedirectToAction("Index", "AdminCategory");
        }
    }
}
