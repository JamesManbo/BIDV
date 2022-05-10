using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Common;

namespace BIDV.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/

        [HttpPost]
        public JsonResult UploadFiles(string folder)
        {
            var now = DateTime.Now;
            var files = Request.Files;
            if (Request.Files.Count <= 0) return Json(new { status = false, msg = "Bạn chưa chọn file." });
            var lstFiles = new List<string>();
            var virtualPath = string.Format("/Content/FrontEnd/_img_server/{0}/{1}/{2}/{3}/", folder, now.Year,
                now.Month, now.Day);
            var path = Server.MapPath(virtualPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            for (var i = 0; i < files.Count; i++)
            {
                var fileData = files[i];
                var filename = files[i].FileName;
                if (System.IO.File.Exists(Path.Combine(path, files[i].FileName)))
                {
                    filename = string.Format("copy({0})_{1}",HelperDateTime.Convert2TimeStamp(now), files[i].FileName);
                }
                fileData.SaveAs(string.Format("{0}/{1}", path, filename));
                lstFiles.Add(string.Format("{0}/{1}", virtualPath, filename));
            }
            return Json(new { status = true, msg = "Upload thành công", files = lstFiles });
        }

        
    }
}
