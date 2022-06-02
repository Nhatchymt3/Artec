using Labixa.ViewModels.AccountManagement;
using Labixa.Views.AccountManagement;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Outsourcing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class PermissionAccountController : Controller
    {
        readonly UserManager<User> _userManager;
        public PermissionAccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public ActionResult Index()
        {
            //không load thằng đang đăng nhập
            ViewBag.userId = _userManager.FindByName(User.Identity.Name).Id;
            var user = _userManager.Users.ToList();
            return View(user);
        }
        #region create account
        public ActionResult CreateAccount()
        {
            return View(new PermissionAccountModel());
        }
        [HttpPost]
        public ActionResult CreateAccount(PermissionAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please enter complete information (*)!");
                return View(model);
            }
            var usr = _userManager.FindByName(model.UserName);
            if (usr != null) ModelState.AddModelError("", "Account existed. Please create again account!");
            else
            {
                if (model.RolePermission == SystemRoles.RoleNone)
                {
                    model.RolePermission = SystemRoles.RoleEmployee;
                }
                var createUsr = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    RoleId = model.RolePermission,
                    Activated = model.Active
                };
                var chkUsr = _userManager.Create(createUsr, model.Password);
                if (chkUsr.Succeeded)
                {
                    var result = _userManager.AddToRole(createUsr.Id, SwapSystemRoles(model.RolePermission));
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "PermissionAccount", new { area = "Admin" });
                    }
                }
                ModelState.AddModelError("", "Creating failed. Oops!");
            }
            return View(model);
        }
        #endregion
        #region Edit Acccount
        public ActionResult EditAccount(string id)
        {
            var usr = _userManager.FindById(id);
            var model = new PermissionEditAccountModel()
            {
                Id = usr.Id,
                UserName = usr.UserName,
                Email = usr.Email,
                Activated = usr.Activated,
                PhoneNumber = usr.PhoneNumber,
                RoleId = usr.RoleId
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult EditAccount(PermissionEditAccountModel model)
        {
            if (ModelState.IsValid)
            {
                var usr = _userManager.FindById(model.Id);
                usr.Activated = model.Activated;
                usr.RoleId = model.RoleId;
                _userManager.Update(usr);
                return RedirectToAction("Index");
            }
            return RedirectToAction("EditAccount");
        }
        #endregion
        #region Delete Account
        public ActionResult DeleteAccount(string id)
        {
            var usr = _userManager.FindById(id);
            var chkUsr = _userManager.Delete(usr);
            return RedirectToAction("Index");
        }
        #endregion
        #region Disable account
        public ActionResult DisableAccount(string id)
        {
            var usr = _userManager.FindById(id);
            usr.Activated = false;
            var chkUsr = _userManager.Update(usr);
            return RedirectToAction("Index");
        }
        #endregion
        #region Enable account
        public ActionResult EnableAccount(string id)
        {
            var usr = _userManager.FindById(id);
            usr.Activated = true;
            var chkUsr = _userManager.Update(usr);
            return RedirectToAction("Index");
        }
        #endregion
        #region change pass account
        public ActionResult ChangePassAccount(string id)
        {
            return View(new PermissionChangePassAccountModel() { Id = id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassAccount(PermissionChangePassAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Oops");
                return View(model);
            }
            var usr = _userManager.FindById(model.Id);
            var result = _userManager.PasswordHasher.HashPassword(model.Password);
            usr.PasswordHash = result;
            await _userManager.UpdateAsync(usr);
            return RedirectToAction("Index");
        }
        #endregion
        #region extendsion roles
        private string SwapSystemRoles(SystemRoles roles)
        {
            var roleStr = string.Empty;
            switch (roles)
            {
                case SystemRoles.RoleNone:
                    roleStr = "none";
                    break;
                case SystemRoles.RoleAdmin:
                    roleStr = "Admin";
                    break;
                case SystemRoles.RoleManger:
                    roleStr = "Manager";
                    break;
                case SystemRoles.RoleEmployee:
                    roleStr = "Employee";
                    break;
                default:
                    break;
            }
            return roleStr;
        }
        #endregion
    }
}