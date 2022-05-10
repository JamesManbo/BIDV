using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Model;
using BIDV.Repository;

namespace BIDV.Controllers
{
    public class InfoWebsiteController : Controller
    {
        //
        // GET: /InfoWebsite/
        readonly SystemRepository _systemRepository = new SystemRepository();
        public ActionResult _Top()
        {
            var info = new bidv__system();
            if (Session["InfoWebsite"] == null)
            {
                info = _systemRepository.GetAll().FirstOrDefault();
                Session["InfoWebsite"] = info;
            }
            else
            {
                info = (bidv__system)Session["InfoWebsite"];
            }
            return View(info);
        }
        public ActionResult _Bottom()
        {
            var info = new bidv__system();
            if (Session["InfoWebsite"] == null)
            {
                info = _systemRepository.GetAll().FirstOrDefault();
                Session["InfoWebsite"] = info;
            }
            else
            {
                info = (bidv__system)Session["InfoWebsite"];
            }
            return View(info);
        }
    }
}
