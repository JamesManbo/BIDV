using BIDV.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDV.Controllers
{
    public class HomeMenuController : Controller
    {
        readonly MenuRepository _menuRepository = new MenuRepository();
        //
        // GET: /HomeMenu/

        public ActionResult Header()
        {
            var menuHeader = _menuRepository.GetAll().Where(a => a.position == 0).ToList();
            return View(menuHeader);
        }
        public ActionResult Footer()
        {
            var menuFooter = _menuRepository.GetAll().Where(a => a.position == 1).OrderBy(b=>b.weight).ToList();
            return View(menuFooter);
        }
    }
}
