using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Model;
using BIDV.Model.CustomModel;
using BIDV.Repository;
using Newtonsoft.Json;

namespace BIDV.Areas.mobile.Controllers
{
    public class CardServiceController : Controller
    {
        //
        // GET: /CardService/
        readonly CategoryRepository _categoryRepository = new CategoryRepository();
        readonly CardRepository _cardRepository = new CardRepository();
        readonly SloganRepository _sloganRepository = new SloganRepository();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListCard(int id)
        {
            //Thẻ đại diện
            var firstCard = _cardRepository.GetWhere(g => g.type_id == id && g.status == 1).OrderBy(g => g.weight).ThenBy(g=>g.title).FirstOrDefault();
            ViewBag.FirstCard = firstCard;
            if (firstCard != null)
            {
                var lstSlogan = _sloganRepository.GetWhere(g => g.card_id == firstCard.id).OrderBy(g => g.index).ToList();
                ViewBag.Slogan = lstSlogan;
            }
            var lstChildCard = _cardRepository.GetWhere(g => g.pid == firstCard.id).ToList();
            if (lstChildCard.Any())
            {
                lstChildCard.Add(firstCard);
                ViewBag.ChildCard = lstChildCard.OrderBy(g => g.weight).ToList();
            }
            var objCat = _categoryRepository.GetById(id);
            return View(objCat);
        }
        public ActionResult Details(int id)
        {
            var objCard = _cardRepository.GetById(id);
            if (objCard != null)
            {
                var lstSlogan = _sloganRepository.GetWhere(g => g.card_id == objCard.id).OrderBy(g => g.index).ToList();
                ViewBag.Slogan = lstSlogan;
                var lstCard =
                 _cardRepository.GetWhere(g => g.type_id == objCard.type_id && g.status == 1 && g.pid == 0).OrderBy(g => g.weight).ToList();
                ViewBag.ListCardSlide = lstCard;
            }
            return View(objCard);
        }
    }
}
