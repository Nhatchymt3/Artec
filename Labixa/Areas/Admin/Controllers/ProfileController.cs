using AutoMapper;
using Labixa.Areas.Admin.ViewModel;
using Outsourcing.Core.Framework.Controllers;
using Outsourcing.Service;
using System;
using System.Web.Mvc;

namespace Labixa.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Manager, Employee")]
    public class ProfileController : Controller
    {
        #region Field
        readonly IProfileService _profileService;
        #endregion

        #region Ctor
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        #endregion
        //
        // GET: /Admin/Profile/
        public ActionResult Index()
        {
            var list = _profileService.GetProfiles();
            return View(list);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var model = new ProfileFormModel();
            return View(model);
        }
        [ValidateInput(false)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(ProfileFormModel obj, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                obj.Deleted = false;
                Outsourcing.Data.Models.Profile item = Mapper.Map<ProfileFormModel, Outsourcing.Data.Models.Profile>(obj);
                item.DateCreated = DateTime.Now;
                item.Role = 0;
                _profileService.CreateProfile(item);
                return continueEditing ? RedirectToAction("Edit", "Profile", new { id = item.Id })
                                 : RedirectToAction("Index", "Profile");
            }
            else return View("Create", obj);
        }
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int profileId)
        {
            var item = _profileService.GetProfileById(profileId);
            ProfileFormModel obj = Mapper.Map<Outsourcing.Data.Models.Profile, ProfileFormModel>(item);
            if (obj != null)
            {
                return View(obj);
            }
            return RedirectToAction("Index", "Profile");
        }
        [ValidateInput(false)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(ProfileFormModel obj, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                Outsourcing.Data.Models.Profile item = Mapper.Map<ProfileFormModel, Outsourcing.Data.Models.Profile>(obj);
                _profileService.EditProfile(item);
                return continueEditing ? RedirectToAction("Edit", "Profile", new { id = item.Id })
                    : RedirectToAction("Index", "Profile");
            }
            else
                return View("Edit", obj);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int profileId)
        {
            _profileService.DeleteProfile(profileId);
            return RedirectToAction("Index", "Profile");
        }
    }
}