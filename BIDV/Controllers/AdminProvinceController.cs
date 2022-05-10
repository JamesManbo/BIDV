using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Common;
using BIDV.Model;
using BIDV.Repository;
using PagedList;
namespace BIDV.Controllers
{
    public class AdminProvinceController : Controller
    {
        readonly ProvinceRepository _provinceRepository = new ProvinceRepository();
       
        //
        // GET: /AdminProvince/

        public ActionResult Index(string title, int page = 1)
        {
            var lstProvince = _provinceRepository.GetAll();
            if (!string.IsNullOrEmpty(title))
            {
                ViewBag.TitleContent = title;
                lstProvince =
                    lstProvince.Where(
                        g =>
                            HelperString.UnsignCharacter(g.title.ToLower().Trim())
                                .Contains(HelperString.UnsignCharacter(title.ToLower().Trim())));
            }
            lstProvince = lstProvince.OrderBy(g => g.title);
            return View(lstProvince.ToPagedList(page, Config.PageSize));
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(bidv___province bidvProvince)
        {
            if (string.IsNullOrEmpty(bidvProvince.title) || bidvProvince.status == null || bidvProvince.is_city == null)
            {
                return RedirectToAction("Add","AdminProvince");
            }
            else
            {
                _provinceRepository.Add(bidvProvince);
                return RedirectToAction("index", "AdminProvince");
            }
           
        }
        public ActionResult Edit(int id)
        {
            var bidvProvince = _provinceRepository.GetById(id);
            if (bidvProvince == null)
            {
                return HttpNotFound();

            }
            return View(bidvProvince);
        }


        [HttpPost]
        
        public ActionResult Edit(bidv___province bidvProvince)
        {
            if (string.IsNullOrEmpty(bidvProvince.title) || bidvProvince.status == null || bidvProvince.is_city == null)
            {
                return RedirectToAction("Edit", "AdminProvince", new {id = bidvProvince.id});
            }
           _provinceRepository.Update(bidvProvince);
            return RedirectToAction("Index");
        }
        public ActionResult Back()
        {

            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id)
        {
            bidv___province bidvProvince = _provinceRepository.GetById(id);
            if (bidvProvince == null)
            {
                return HttpNotFound();
            }
            _provinceRepository.Delete(bidvProvince);
            return RedirectToAction("Index");

        }
        public ActionResult DeleteAll(string listId)
        {
            var arrId = listId.Split(',');
            foreach (var id in arrId)
            {

                bidv___province bidvProvince = _provinceRepository.GetById(Int32.Parse(id));
                if (bidvProvince == null)
                {
                    return HttpNotFound();
                }
                _provinceRepository.Delete(bidvProvince); 
            }
           
            return RedirectToAction("Index");

        }


    }
}
