using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Common;
using BIDV.Model;
using BIDV.Repository;
using PagedList;
using System.Web.Helpers;

namespace BIDV.Controllers
{
    [Authorize]
    public class AdminAttachmentController : BaseController
    {
        //
        // GET: /AdminAttachment/
        readonly AttachmentRepository _attachmentRepository = new AttachmentRepository();
        readonly DetailAttachmentRepository _detailAttachmentRepository = new DetailAttachmentRepository();
        public ActionResult Index(int? id, int page = 1)
        {
            TempData["CategoryID"] = id;
            return Json(RenderViewToString("~/Views/AdminAttachment/Index.cshtml"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListData(string filename, int catid, int page = 1)
        {
            var lstAttachment = _attachmentRepository.GetAll().Where(g => g.status == 1);
            if (!string.IsNullOrEmpty(filename))
            {
                lstAttachment =
                    lstAttachment.Where(
                        g =>
                            HelperString.UnsignCharacter(g.name.ToLower().Trim())
                                .Contains(HelperString.UnsignCharacter(filename.ToLower().Trim())));
            }
            var totalAttachment = lstAttachment.Count();
            lstAttachment = lstAttachment.OrderByDescending(g => g.created).Skip((page - 1) * Config.PageSize).Take(Config.PageSize);
            Session["CountFile"] = totalAttachment;
            return Json(new
            {
                viewContent = RenderViewToString("~/Views/AdminAttachment/_ListData.cshtml", lstAttachment),
                totalPages = Math.Ceiling(((double)totalAttachment / Config.PageSize)),
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AttachmentManager(string filename, int page = 1)
        {
            var listAttach = _attachmentRepository.GetAll().Where(a => a.status == 1);
            if (!string.IsNullOrEmpty(filename))
            {
                ViewBag.SearchContent = filename;
                listAttach = listAttach.Where(a => a.name.Contains(filename));
            }
            listAttach = listAttach.OrderByDescending(a => a.created);
            return View(listAttach.ToPagedList(page, Config.PageSize));
        }
        [HttpPost]
        public ActionResult Upload(string newname, int page = 1)
        {
            if (Request.Files.Count == 0)
            {
                return Json(new
                {
                    mess = "Bạn chưa chọn file upload",
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
            var file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {
                var arr = file.FileName.Split('.');
                var ext = arr[arr.Length - 1];
                var now = DateTime.Now;
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                //	http://bidv.ezcms.org/_img_server/files/2016/08/09/Danh_sach_MSDT_tuan_3.pdf

                if (!string.IsNullOrEmpty(newname))
                {
                    fileName = newname;
                }
                var dir =
                    Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/files/{0}/{1}/{2}", now.Year, now.Month,
                        now.Day));
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                var path = Path.Combine(dir, fileName);
                file.SaveAs(path);
                var objAttach = new bidv__attach
                {
                    name = fileName,
                    ext = ext,
                    type = file.ContentType,
                    size = file.ContentLength,
                    status = 1,
                    created = (int?)HelperDateTime.Convert2TimeStamp(now)
                };
                _attachmentRepository.Add(objAttach);

                var lstAttachment = _attachmentRepository.GetAll().Where(g => g.status == 1);
                var totalAttachment = lstAttachment.Count();
                lstAttachment = lstAttachment.OrderByDescending(g => g.created).Skip((page - 1) * Config.PageSize).Take(Config.PageSize);
                Session["CountFile"] = totalAttachment;
                return Json(new
                {
                    viewContent = RenderViewToString("~/Views/AdminAttachment/_ListData.cshtml", lstAttachment),
                    totalPages = Math.Ceiling(((double)totalAttachment / Config.PageSize)),
                    mess = "Upload thành công",
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                mess = "Upload không thành công",
                status = false
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetListAttachment(int object_id, string object_type)
        {
            var lstDetailAttachment =
                _detailAttachmentRepository.GetWhere(g => g.object_id == object_id && g.object_type == object_type).ToList();
            return Json(RenderViewToString("~/Views/AdminAttachment/_ListFileChoosen.cshtml", lstDetailAttachment.ToList()), JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddListAttachment(string arrAttachId, int object_id, string object_type)
        {
            var now = DateTime.Now;
            var arrid = arrAttachId.Split(',').Select(int.Parse);
            var enumerable = arrid as int[] ?? arrid.ToArray();
            foreach (var attachid in enumerable)
            {
                var obj =
                    _detailAttachmentRepository.GetWhere(
                        g => g.attach_id == attachid && g.object_id == object_id && g.object_type == object_type).FirstOrDefault();
                if (obj == null)
                {
                    var objdetail = new bidv__attach_detail();
                    objdetail.attach_id = attachid;
                    objdetail.created = (int?)HelperDateTime.Convert2TimeStamp(now);
                    objdetail.object_type = object_type;
                    objdetail.object_id = object_id;
                    _detailAttachmentRepository.Add(objdetail);
                }
            }
            return RedirectToAction("GetListAttachment", "AdminAttachment", new { object_id = object_id, object_type = object_type });
        }

        public ActionResult DeleteAttachment(int id, int object_id, string object_type)
        {
            var obj = _detailAttachmentRepository.GetById(id);
            _detailAttachmentRepository.Delete(obj);
            var lstDetailAttachment =
                _detailAttachmentRepository.GetWhere(g => g.object_id == object_id && g.object_type == object_type).ToList();
            return Json(RenderViewToString("~/Views/AdminAttachment/_ListFileChoosen.cshtml", lstDetailAttachment.ToList()), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteFile(int id)
        {
            var fileObj = _attachmentRepository.GetById(id);
            if (fileObj != null)
            {
                var lstobj = _detailAttachmentRepository.GetWhere(a => a.attach_id == fileObj.id).ToList();
                if (lstobj.Count > 0)
                {
                    foreach (var item in lstobj)
                    {
                        _detailAttachmentRepository.Delete(item);
                    }

                }
                _attachmentRepository.Delete(fileObj);
            }
            return RedirectToAction("AttachmentManager", "AdminAttachment");
        }
        public ActionResult DeleteAttachDetail(int id)
        {
            var obj = _detailAttachmentRepository.GetById(id);
            _detailAttachmentRepository.Delete(obj);
            return RedirectToAction("AttachmentManager", "AdminAttachment");
        }
        [HttpPost]
        public ActionResult UpdateFile(int fileID, HttpPostedFileBase file)
        {
            var obj = _attachmentRepository.GetById(fileID);
            if (obj != null)
            {
                var currentFile = file;
                if (!string.IsNullOrEmpty(obj.name) && currentFile != null)
                {
                    if (obj.created != null)
                    {
                        var oldDate = HelperDateTime.ConvertTimespan2DateTime(obj.created.Value);
                        var path = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/files/{0}/{1}/{2}/", oldDate.Year, oldDate.Month, oldDate.Day));
                        var pathfile = path + obj.name + "." + obj.ext;
                        if (System.IO.File.Exists(pathfile))
                        {
                            System.IO.File.Delete(pathfile);
                        }
                        var now = DateTime.Now;
                        var timestamp = HelperDateTime.Convert2TimeStamp(now);
                        var name = file.FileName.Split('.')[0];
                        var ext = file.FileName.Split('.')[1];
                        var filename = string.Format("{0}{1}.{2}", name, "", ext);
                        var path2 = Server.MapPath(string.Format("~/Content/FrontEnd/_img_server/files/{0}/{1}/{2}/", now.Year, now.Month, now.Day));
                        var pathfile2 = path2 + name + "." + ext;
                        obj.name = filename;
                        obj.created = (int?)timestamp;
                        obj.ext = ext;
                        obj.type = file.ContentType;
                        obj.size = file.ContentLength;
                        if (!Directory.Exists(path2))
                        {
                            Directory.CreateDirectory(path2);
                        }
                        file.SaveAs(pathfile2);
                        _attachmentRepository.Update(obj);
                    }
                }
            }

            return RedirectToAction("AttachmentManager", "AdminAttachment");
        }
    }
}
