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
    public class LocationController : Controller
    {
        #region Field
        readonly ILocationService _LocationService;
        #endregion

        #region Ctor
        public LocationController(ILocationService LocationService)
        {
            this._LocationService = LocationService;
        }
        #endregion
        //
        // GET: /Admin/Location/
        public ActionResult Index()
        {
            var list = _LocationService.GetLocations();
            return View(list);
        }
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Create()
        {
            ViewBag.Location = _LocationService.GetLocations();
            var model = new LocationFormModel();
            return View(model);
        }
        [ValidateInput(false)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(LocationFormModel obj, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                obj.Deleted = false;
                Location item = Mapper.Map<LocationFormModel, Location>(obj);
                _LocationService.CreateLocation(item);
                return continueEditing ? RedirectToAction("Edit", "Location", new { LocationId = item.Id })
                                 : RedirectToAction("Index", "Location");
            }
            else return View("Create", obj);
        }
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int LocationId)
        {
            var item = _LocationService.GetLocationById(LocationId);
            LocationFormModel obj = Mapper.Map<Location, LocationFormModel>(item);
            if (obj != null)
            {
                return View(obj);
            }
            return RedirectToAction("Index", "Location");
        }
        [ValidateInput(true)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(LocationFormModel obj, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                Location item = Mapper.Map<LocationFormModel, Location>(obj);
                _LocationService.EditLocation(item);
                return continueEditing ? RedirectToAction("Edit", "Location", new { LocationId = item.Id })
                    : RedirectToAction("Index", "Location");
            }
            else
                return View("Edit", obj);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int LocationId)
        {
            _LocationService.DeleteLocation(LocationId);
            return RedirectToAction("Index", "Location");
        }
    }
}