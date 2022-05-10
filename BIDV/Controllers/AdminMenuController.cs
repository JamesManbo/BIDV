using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Model;
using BIDV.Repository;
using PagedList;
namespace BIDV.Controllers
{
    public class AdminMenuController : Controller
    {
        readonly MenuRepository _menuRepository = new MenuRepository();
        //
        // GET: /AdminMenu/

        public ActionResult Index(int page = 1)
        {
            var listMenu = _menuRepository.GetAll().ToList();
            ViewBag.ListMenu = listMenu;
            return View(listMenu);
        }
        public ActionResult AdminMenuLef()
        {
            return View();
        }
        public ActionResult Add()
        {
            var listMenu = _menuRepository.GetAll();
            ViewBag.ListMenu = listMenu;
            return View();
        }
        [HttpPost]
        public ActionResult Add( bidv__menu bidvMenu)
        {
            _menuRepository.Add(bidvMenu);
            return RedirectToAction("Index","AdminMenu");
        }
        public ActionResult Edit (int Id)
        {
            var obj = _menuRepository.GetById(Id);
            var listMenu = _menuRepository.GetAll();
            ViewBag.ListMenu = listMenu;
            return View(obj);
        }

         [HttpPost]
        public ActionResult Edit(bidv__menu bidvMenu)
        {
            _menuRepository.Update(bidvMenu);
            return RedirectToAction("Index", "AdminMenu");

        }

        public ActionResult Delete(int Id)
         {
             bidv__menu bidvMenu = _menuRepository.GetById(Id);
             if (bidvMenu == null)
             {
                 return HttpNotFound();
             }
             _menuRepository.Delete(bidvMenu);
             return RedirectToAction("Index");
        }
    }
}
