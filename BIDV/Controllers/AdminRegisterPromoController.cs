using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Common;
using BIDV.Model;
using BIDV.Repository;
using PagedList;

namespace BIDV.Controllers
{
    
    public class AdminRegisterPromoController : Controller
    {
        //
        // GET: /AdminRegisterPromo/
        readonly  SubscribeRepository _subscribeRepository = new SubscribeRepository();
        [Authorize]
        public ActionResult Index(string email, DateTime? fromdate, DateTime? todate, int status = 1, int page = 1)
        {
            var lstRegPromo = _subscribeRepository.GetAll();
            if (status != -99)
            {
                if (status != 1)
                {
                    lstRegPromo = lstRegPromo.Where(g => g.status == status);
                }
                else
                {
                    lstRegPromo = lstRegPromo.Where(g => g.status == 1);
                }
            }
            if (!string.IsNullOrEmpty(email))
            {
                lstRegPromo = lstRegPromo.Where(g => g.email.Contains(email.Trim().ToLower()));
            }
            if (fromdate != null)
            {
                lstRegPromo = lstRegPromo.Where(g => g.created >= HelperDateTime.Convert2TimeStamp(fromdate.Value));
            }
            if (todate != null)
            {
                lstRegPromo = lstRegPromo.Where(g => g.created <= HelperDateTime.Convert2TimeStamp(todate.Value));
            }
            lstRegPromo = lstRegPromo.OrderByDescending(g => g.created);
            ViewBag.email = email;
            ViewBag.fromdate = fromdate;
            ViewBag.todate = todate;
            ViewBag.status = status;
            return View(lstRegPromo.ToPagedList(page,20));
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            var objRegPromo = _subscribeRepository.GetById(id);
            objRegPromo.status = -1;
            _subscribeRepository.Update(objRegPromo);
            return RedirectToAction("Index", "AdminRegisterPromo");
        }
        [Authorize]
        public ActionResult UpdateStatus(int id)
        {
            var objRegPromo = _subscribeRepository.GetById(id);
            if (objRegPromo.status == 1)
            {
                objRegPromo.status = 0;
            }
            else if (objRegPromo.status == 0)
            {
                objRegPromo.status = 1;
            }
            _subscribeRepository.Update(objRegPromo);
            return Json(objRegPromo.status, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(string email)
        {
            var findEmail = _subscribeRepository.GetWhere(g => g.email == email);
            if (findEmail.Any())
            {
                return Json( new {status = false, msg = "Bạn đã đăng ký theo dõi rồi"}, JsonRequestBehavior.AllowGet);
            }
            var obj = new bidv__subscribe
            {
                email = email,
                created = (int) HelperDateTime.Convert2TimeStamp(DateTime.Now),
                status = 1,
                name = "",
                phone = "",
                age = 0,
                city_id = 0,
                sendTime = 0
            };
            _subscribeRepository.Add(obj);
            return Json(new { status = true, msg = "Đăng ký thành công" }, JsonRequestBehavior.AllowGet);
        }
    }
}
