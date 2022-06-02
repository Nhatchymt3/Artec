using AutoMapper;
using Labixa.Areas.Admin.ViewModel;
using Outsourcing.Core.Framework.Controllers;
using Outsourcing.Data.Models;
using Outsourcing.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Manager, Employee")]
    public class PetProfileController : Controller
    {
        #region Field
        readonly IPetProfileService _petProfileService;
        readonly IProfileService _profileService;
        #endregion

        #region Ctor
        public PetProfileController(IPetProfileService petProfileService, IProfileService profileService)
        {
            this._petProfileService = petProfileService;
            this._profileService = profileService;
        }
        #endregion
        //
        // GET: /Admin/PetProfile/
        public ActionResult Index()
        {
            var list = _petProfileService.GetPetProfiles();
            return View(list);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.users = _profileService.GetProfiles();
            var model = new PetProfileFormModel();
            return View(model);
        }
        [ValidateInput(false)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(PetProfileFormModel obj, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                obj.Deleted = false;
                PetProfile item = Mapper.Map<PetProfileFormModel, PetProfile>(obj);
                item.LastUpdate = DateTime.Now;
                _petProfileService.CreatePetProfile(item);
                return continueEditing ? RedirectToAction("Edit", "PetProfile", new { id = item.Id })
                                 : RedirectToAction("Index", "PetProfile");
            }
            else return View("Create", obj);
        }
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int petprofileId)
        {
            var item = _petProfileService.GetPetProfileById(petprofileId);
            PetProfileFormModel obj = Mapper.Map<PetProfile, PetProfileFormModel>(item);
            if (obj != null)
            {
                return View(obj);
            }
            return RedirectToAction("Index", "Profile");
        }
        [ValidateInput(false)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(PetProfileFormModel obj, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                PetProfile item = Mapper.Map<PetProfileFormModel, PetProfile>(obj);
                _petProfileService.EditPetProfile(item);
                return continueEditing ? RedirectToAction("Edit", "PetProfile", new { id = item.Id })
                    : RedirectToAction("Index", "PetProfile");
            }
            else
                return View("Edit", obj);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int petprofileId)
        {
            _petProfileService.DeletePetProfile(petprofileId);
            return RedirectToAction("Index", "PetProfile");
        }
    }
}