using System;
using System.Collections.Generic;
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
    public class AdminSystemController : Controller
    {
        //
        // GET: /AdminSystem/
        readonly SystemRepository _systemRepository = new SystemRepository();
        public ActionResult Index()
        {
            var infowebsite = _systemRepository.GetAll().FirstOrDefault();
            return View(infowebsite);
        }

        [HttpPost]
        public ActionResult Index(bidv__system item, HttpPostedFileBase filelogo, HttpPostedFileBase filefavicon)
        {
            var now = DateTime.Now;
            var timestamp = HelperDateTime.Convert2TimeStamp(now);
            if (filelogo != null)
            {
                var image = new WebImage(filelogo.InputStream);
                var name = filelogo.FileName.Split('.')[0];
                var ext = filelogo.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Trim(), timestamp, ext);
                var path = Server.MapPath(string.Format("/Content/FrontEnd/_img_server/siteInfo/"));
                item.logo = filename;
                HelperImages.SaveAndResize(image, 256,32, filename, path);
            }
            if (filefavicon != null)
            {
                var image = new WebImage(filefavicon.InputStream);
                var name = filefavicon.FileName.Split('.')[0];
                var ext = filefavicon.FileName.Split('.')[1];
                var filename = string.Format("{0}_{1}.{2}", HelperString.UnsignCharacter(name).Trim(), timestamp, ext);
                var path = Server.MapPath(string.Format("/Content/FrontEnd/_img_server/siteInfo/"));
                item.favicon = filename;
                HelperImages.SaveAndResize(image, 16,16, filename, path);
            }
            _systemRepository.Update(item);
            Session["InfoWebsite"] = null;
            return View(item);
        }
    }
}
