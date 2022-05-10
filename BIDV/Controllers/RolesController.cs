using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.BaseSecurity;
using BIDV.Model;
using BIDV.Repository;

namespace BIDV.Controllers
{
    [CustomAuthorize(Config.TypeUser.Admin, Config.TypeUser.SuperAdmin)]
    public class RolesController : BaseController
    {
        //
        // GET: /Roles/
        readonly RolesRepository _rolesRepository = new RolesRepository();
        readonly UserRepository _userRepository = new UserRepository();
        readonly UsersInRolesRepository _usersInRolesRepository = new UsersInRolesRepository();
        public ActionResult Index()
        {
            var lstRoles = _rolesRepository.GetAll();
            return View(lstRoles);
        }

        public ActionResult Edit(int id)
        {
            var lstRoles = _rolesRepository.GetAll();
            var objRole = _rolesRepository.GetById(id);
            ViewBag.Role = objRole;
            return View(lstRoles);
        }

        [HttpPost]
        public ActionResult Edit(Model.webpages_Roles item)
        {
            _rolesRepository.Update(item);
            return RedirectToAction("Index", "Roles");
        }

        [HttpPost]
        public ActionResult Add(Model.webpages_Roles item)
        {
            _rolesRepository.Add(item);
            return RedirectToAction("Index", "Roles");
        }

        public ActionResult Delete(int id)
        {
            var item = _rolesRepository.GetById(id);
            _rolesRepository.Delete(item);
            return RedirectToAction("Index", "Roles");
        }
        public ActionResult AddUserToGroup(int id)
        {
            var objGroupUser = _rolesRepository.GetById(id);
            //var lstmembership = _u
            var lstUser = _userRepository.GetAllViewUserProfiles().Where(g => g.Status == (int)Config.StatusUser.Active).ToList();
            //lấy những user đã được thêm vào nhóm
            var lstUserIdAdded = _usersInRolesRepository.GetWhere(g => g.RoleId == id).Select(h => h.UserId).ToList();
            var lstUserAdded = lstUser.Where(g => lstUserIdAdded.Contains(g.UserId)).ToList();
            //danh sách user chưa được thêm vào nhóm
            var lstUserUnAdd = lstUser.Where(g => !lstUserIdAdded.Contains(g.UserId)).ToList();
            TempData["lstUserAdded"] = lstUserAdded;
            TempData["lstUserUnAdd"] = lstUserUnAdd;
            return Json(RenderViewToString("~/Views/Roles/_AddUserToGroup.cshtml", objGroupUser),
                JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddUserToGroup(int[] to, int RoleId)
        {
            try
            {
                //xóa hết user khỏi nhóm
                _usersInRolesRepository.Delete(RoleId);
            }
            catch (Exception)
            {
                return Json(new
                {
                    IsSuccess = false,
                    Messenger = string.Format("Có lỗi xảy ra")
                }, JsonRequestBehavior.AllowGet);
            }

            //thêm lại danh sách user vào nhóm
            if (to != null)
            {
                try
                {
                    var lstUserInRoles = new List<webpages_UsersInRoles>();
                    foreach (var UserId in to)
                    {
                        var obj = new webpages_UsersInRoles();
                        obj.UserId = UserId;
                        obj.RoleId = RoleId;
                        lstUserInRoles.Add(obj);
                    }
                    _usersInRolesRepository.Add(lstUserInRoles);
                }
                catch (Exception)
                {
                    return Json(new
                    {
                        IsSuccess = false,
                        Messenger = string.Format("Có lỗi xảy ra")
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    IsSuccess = true,
                    Messenger = string.Format("Thêm người dùng vào nhóm thành công")
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                IsSuccess = true,
                Messenger = string.Format("Cập nhật thành công")
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
