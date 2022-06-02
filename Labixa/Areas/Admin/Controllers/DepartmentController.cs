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
    public class DepartmentController : Controller
    {
        #region Field
        readonly IDepartmentService _DepartmentService;
        #endregion
         
         
        #region Ctor
        public DepartmentController(IDepartmentService DepartmentService)
        {
            this._DepartmentService = DepartmentService;
        }
        #endregion
        //
        // GET: /Admin/Department/
        public ActionResult Index()
        {
            var list = _DepartmentService.GetDepartments();
            return View(list);
        }
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Create()
        {
            ViewBag.department = _DepartmentService.GetDepartments();
            var model = new DepartmentFormModel();
            return View(model);
        }
        [ValidateInput(false)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(DepartmentFormModel obj, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                obj.Deleted = false;
                Department item = Mapper.Map<DepartmentFormModel, Department>(obj);



                _DepartmentService.CreateDepartment(item);
                return continueEditing ? RedirectToAction("Edit", "Department", new { DepartmentId = item.Id })
                                 : RedirectToAction("Index", "Department");
            }
            else return View("Create", obj);
        }
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int DepartmentId)
        {
            var item = _DepartmentService.GetDepartmentById(DepartmentId);
            DepartmentFormModel obj = Mapper.Map<Department, DepartmentFormModel>(item);
            if (obj != null)
            {
                return View(obj);
            }
            return RedirectToAction("Index", "Department");
        }
        [ValidateInput(false)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(DepartmentFormModel obj, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                Department item = Mapper.Map<DepartmentFormModel, Department>(obj);
                _DepartmentService.EditDepartment(item);
                return continueEditing ? RedirectToAction("Edit", "Department", new { DepartmentId = item.Id })
                    : RedirectToAction("Index", "Department");
            }
            else
                return View("Edit", obj);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int DepartmentId)
        {
            _DepartmentService.DeleteDepartment(DepartmentId);
            return RedirectToAction("Index", "Department");
        }
    }
}