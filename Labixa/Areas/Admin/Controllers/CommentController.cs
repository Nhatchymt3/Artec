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
    public class CommentController : Controller
    {
        #region Field
        readonly ICommentService _commentService;
        readonly IProfileService _profileService;
        #endregion

        #region Ctor
        public CommentController(ICommentService commentService, IProfileService profileService)
        {
            _commentService = commentService;
            _profileService = profileService;
        }
        #endregion
        //
        // GET: /Admin/Comment/
        public ActionResult Index()
        {
            var list = _commentService.GetComments();
            return View(list);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var model = new CommentFormModel();
            return View(model);
        }
        [ValidateInput(false)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(CommentFormModel obj, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                obj.Deleted = false;
                Comment item = Mapper.Map<CommentFormModel, Comment>(obj);
                item.DateCreate = DateTime.Now;
                var profile = _profileService.GetProfileById(item.UserId);
                item.UserName = profile.LastName + " " + profile.FirstName;
                _commentService.CreateComment(item);
                return continueEditing ? RedirectToAction("Edit", "Comment", new { id = item.Id })
                                 : RedirectToAction("Index", "Comment");
            }
            else return View("Create", obj);
        }
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int commentId)
        {
            var item = _commentService.GetCommentById(commentId);
            CommentFormModel obj = Mapper.Map<Comment, CommentFormModel>(item);
            if (obj != null)
            {
                return View(obj);
            }
            return RedirectToAction("Index", "Comment");
        }
        [ValidateInput(false)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(CommentFormModel obj, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                Comment item = Mapper.Map<CommentFormModel, Comment>(obj);
                _commentService.EditComment(item);
                return continueEditing ? RedirectToAction("Edit", "Comment", new { id = item.Id })
                    : RedirectToAction("Index", "Comment");
            }
            else
                return View("Edit", obj);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int commentId)
        {
            _commentService.DeleteComment(commentId);
            return RedirectToAction("Index", "Comment");
        }
    }
}