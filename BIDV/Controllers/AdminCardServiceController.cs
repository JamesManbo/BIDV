using System;
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
    public class AdminCardServiceController : BaseController
    {
        //
        // GET: /AdminCardService/
        readonly CategoryRepository _categoryRepository = new CategoryRepository();
        readonly CardRepository _cardRepository = new CardRepository();
        readonly SloganRepository _sloganRepository = new SloganRepository();
        public ActionResult Index(int type_id = 0, int status = 0, int page = 1)
        {
            var lstCard = _cardRepository.GetAll();
            if (status != 0)
            {
                lstCard = lstCard.Where(g => g.status == status);
            }
            else
            {
                lstCard = lstCard.Where(g => g.status == 1);
            }
            if (type_id > 0)
            {
                lstCard = lstCard.Where(g => g.type_id == type_id);
            }
            lstCard = lstCard.OrderBy(g => g.weight);
            var lstCategory = _categoryRepository.GetWhere(g => g.type == 0 && g.status == 1).OrderBy(g => g.weight).ToList();
            TempData["ListCategory"] = lstCategory;
            ViewBag.type_id = type_id;
            ViewBag.status = status;
            return View(lstCard.ToPagedList(page, Config.PageSize));
        }

        public ActionResult Add()
        {
            var lstCard = _categoryRepository.GetWhere(g => g.type == 0 && g.status == 1).OrderBy(g => g.weight).ToList();
            TempData["ListCard"] = lstCard;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(bidv__card item, HttpPostedFileBase file)
        {
            if (item.type_id <= 0 || item.type_id == null || string.IsNullOrEmpty(item.title) ||
                string.IsNullOrEmpty(item.title_text))
            {
                return RedirectToAction("Add", "AdminCardService");
            }
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            var image = WebImage.GetImageFromRequest("file");
            if (image != null)
            {
                var name = file.FileName.Split('.')[0];
                var ext = file.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", name, timestamp, ext);
                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/card/{0}/{1}/{2}/size280", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                    now.Day < 10 ? "0" + now.Day : now.Day.ToString()));
                    item.image = filename;
                    HelperImages.SaveAndResizeImage(image, 280, filename, path);
                item.created = (int)HelperDateTime.Convert2TimeStamp(now);
                item.status = 1;
            }

            _cardRepository.Add(item);
            return RedirectToAction("Edit", "AdminCardService", new { id = item.id });
        }

        public ActionResult Edit(int id)
        {
            var lstCard = _categoryRepository.GetWhere(g => g.type == 0 && g.status == 1).OrderBy(g => g.weight).ToList();
            TempData["ListCard"] = lstCard;
            var objCard = _cardRepository.GetById(id);
            return View(objCard);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(bidv__card item, HttpPostedFileBase file)
        {
            if (item.type_id <= 0 || item.type_id == null || string.IsNullOrEmpty(item.title))
            {
                return RedirectToAction("Edit", "AdminCardService", new { id = item.id });
            }
            var now = DateTime.Now;
            var image = WebImage.GetImageFromRequest("file");
            if (image != null)
            {
                int sourceWidth = image.Width;
                int sourceHeight = image.Height;
                float nPercent = ((float)sourceWidth / (float)280);
                int destHeight = (int)(sourceHeight / nPercent);
                image.Resize(280, destHeight);
                var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/card/{0}/{1}/{2}/size280", now.Year, now.Month < 10 ? "0" + now.Month : now.Month.ToString(),
                    now.Day < 10 ? "0" + now.Day : now.Day.ToString()));

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var pathsave = "";
                if (System.IO.File.Exists(Path.Combine(path, file.FileName)))
                {
                    pathsave = Path.Combine(path, string.Format("copy_{0}_", HelperDateTime.Convert2TimeStamp(now)) + file.FileName);
                    item.image = string.Format("copy_{0}_", HelperDateTime.Convert2TimeStamp(now)) + file.FileName;
                }
                else
                {
                    pathsave = Path.Combine(path, file.FileName);
                    item.image = file.FileName;
                }
                image.Save(pathsave); //Lưu ảnh trên server

            }
            item.status = 1;
            item.created = (int)HelperDateTime.Convert2TimeStamp(now);

            _cardRepository.Update(item);
            return RedirectToAction("Index", "AdminCardService");
        }
        public ActionResult Delete(int id)
        {
            var item = _cardRepository.GetById(id);
            item.status = -1;
            _cardRepository.Update(item);
            return RedirectToAction("Index", "AdminCardService");
        }
        public ActionResult AddSlogan(int cardId)
        {
            TempData["cardId"] = cardId;
            var lstSlogan = _sloganRepository.GetWhere(g => g.card_id == cardId).ToList();
            return Json(RenderViewToString("~/Views/AdminCardService/AddSlogan.cshtml", lstSlogan), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddSlogan(bidv__slogan item)
        {
            _sloganRepository.Add(item);
            var lstSlogan = _sloganRepository.GetWhere(g => g.card_id == item.card_id).ToList();
            return Json(RenderViewToString("~/Views/AdminCardService/AddSlogan.cshtml", lstSlogan), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditSlogan(int id)
        {
            var objSlogan = _sloganRepository.GetById(id);
            TempData["ObjSlogan"] = objSlogan;
            var lstSlogan = _sloganRepository.GetWhere(g => g.card_id == objSlogan.card_id).ToList();
            return Json(RenderViewToString("~/Views/AdminCardService/EditSlogan.cshtml", lstSlogan), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditSlogan(bidv__slogan item)
        {
            _sloganRepository.Update(item);
            TempData["ObjSlogan"] = item;
            var lstSlogan = _sloganRepository.GetWhere(g => g.card_id == item.card_id).ToList();
            return Json(RenderViewToString("~/Views/AdminCardService/EditSlogan.cshtml", lstSlogan), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSlogan(int id)
        {
            var obj = _sloganRepository.GetById(id);
            var card_id = obj.card_id;
            _sloganRepository.Delete(obj);
            var lstSlogan = _sloganRepository.GetWhere(g => g.card_id == card_id).ToList();
            return Json(RenderViewToString("~/Views/AdminCardService/AddSlogan.cshtml", lstSlogan), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GroupCard(int catgoryId)
        {
            var lstGroupCard = _cardRepository.GetWhere(g => g.type_id == catgoryId && g.status == 1);
            var lstJson = from bidvCard in lstGroupCard select new { id = bidvCard.id, title = bidvCard.title };
            return Json(lstJson.ToList(), JsonRequestBehavior.AllowGet);
        }


    }
}
