using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BIDV.BaseSecurity;
using BIDV.Filters;
using BIDV.Model;
using BIDV.Models;
using BIDV.Repository;
using PagedList;
using WebMatrix.WebData;

namespace BIDV.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        readonly UserRepository _userRepository = new UserRepository();
        readonly MemberShipRepository _membershipRepository = new MemberShipRepository();
        readonly RolesRepository _rolesRepository = new RolesRepository();
        readonly UsersInRolesRepository _usersInRolesRepository = new UsersInRolesRepository();
        [CustomAuthorize(Config.TypeUser.Admin, Config.TypeUser.SuperAdmin)]
        public ActionResult Index(string username, int page = 1)
        {
            ViewBag.UserName = username;
            var lstUser = _userRepository.GetAllViewUserProfiles();
            if (!string.IsNullOrEmpty(username))
            {
                lstUser = lstUser.Where(g => g.UserName.Contains(username));
            }
            lstUser = lstUser.OrderByDescending(g => g.UserId);
            var lstRoles = _rolesRepository.GetAll().ToList();
            ViewBag.Roles = lstRoles;
            var lstUserInRoles = _usersInRolesRepository.GetViewUserInRoles().ToList();
            ViewBag.ListUserInRoles = lstUserInRoles.ToList();
            return View(lstUser.ToPagedList(page, Config.PageSize));
        }
        [CustomAuthorize(Config.TypeUser.Admin, Config.TypeUser.SuperAdmin)]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [InitializeSimpleMembership]
        [CustomAuthorize(Config.TypeUser.Admin, Config.TypeUser.SuperAdmin)]
        public ActionResult Add(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    return RedirectToAction("Index", "User");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var objUser = _userRepository.GetById(id);
            var objMembership = _membershipRepository.GetById(id);
            ViewBag.UserName = objUser.UserName;
            return View(objMembership);
        }
        [HttpPost]
        public ActionResult Edit(webpages_Membership objUser)
        {
            var oldObj = _membershipRepository.GetById(objUser.UserId);
            oldObj.Status = objUser.Status;
            oldObj.UserType = objUser.UserType;
            _membershipRepository.Update(oldObj);
            return RedirectToAction("Index", "User");
        }
        [CustomAuthorize(Config.TypeUser.Admin, Config.TypeUser.SuperAdmin)]
        public ActionResult Delete(int id)
        {
            var userMembership = _membershipRepository.GetById(id);
            userMembership.Status = (int)Config.StatusUser.Delete;
            _membershipRepository.Update(userMembership);
            return RedirectToAction("Index", "User");
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
