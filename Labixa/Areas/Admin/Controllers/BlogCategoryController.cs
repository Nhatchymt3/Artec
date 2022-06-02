using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Labixa.Areas.Admin.ViewModel;
using Outsourcing.Service;
using Outsourcing.Data.Models;
using Outsourcing.Core.Common;
using Outsourcing.Core.Extensions;
using WebGrease.Css.Extensions;
using Outsourcing.Core.Framework.Controllers;


namespace Labixa.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Manager, Employee")]
    public class BlogCategoryController : Controller
    {
        #region Field
        IBlogCategoryService _blogCategoryService;
        #endregion

        #region Ctor
        public BlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            this._blogCategoryService = blogCategoryService;
        }
        #endregion
        // GET: Admin/BlogCategory
        public ActionResult Index()
        {
            var list = _blogCategoryService.GetBlogCategories().Where(s=> s.Id == 15 || s.Id == 16 || s.Id == 7 || s.Id == 17 || s.Id == 17
            || s.Id == 18 || s.Id == 19 || s.Id == 20);
            return View(model: list);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var listCategory = _blogCategoryService.GetBlogCategories().Where(s => s.Id == 15 || s.Id == 16 || s.Id == 7 || s.Id == 17 || s.Id == 17
           || s.Id == 18 || s.Id == 19 || s.Id == 20).ToSelectListItems(-1);
            var list = new BlogCategoryFormModel { ListCategory = listCategory };
            return View(list);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(BlogCategoryFormModel obj, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                BlogCategory blog = Mapper.Map<BlogCategoryFormModel, BlogCategory>(obj);
                var slug = StringConvert.ConvertShortName(blog.Name);
                blog.Slug = slug;
                _blogCategoryService.CreateBlogCategory(blog);
                return continueEditing ? RedirectToAction("Edit", "BlogCategory", new { Id = blog.Id })
                                : RedirectToAction("Index", "BlogCategory");
            }
            else
            {
                var listCategory = _blogCategoryService.GetBlogCategories().ToSelectListItems(-1);
                obj.ListCategory = listCategory;
                return View("Create", obj);
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int Id)
        {
            var item = _blogCategoryService.GetBlogCategoryById(Id);

            var list = _blogCategoryService.GetBlogCategories().Where(s => s.Id == 15 || s.Id == 16 || s.Id == 7 || s.Id == 17 || s.Id == 17
           || s.Id == 18 || s.Id == 19 || s.Id == 20).ToSelectListItems(int.Parse(item.CategoryParentId.ToString() == "" ? "0" : item.CategoryParentId.ToString()));

            //BlogCategoryFormModel model1 = Mapper.Map<BlogCategory,BlogCategoryFormModel>(item);
            var blogCategory = Mapper.Map<BlogCategory, BlogCategoryFormModel>(item);
            blogCategory.ListCategory = list;

            return View(blogCategory);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(BlogCategoryFormModel obj, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                BlogCategory item = Mapper.Map<BlogCategoryFormModel, BlogCategory>(obj);
                item.Slug = StringConvert.ConvertShortName(item.Name);
                _blogCategoryService.EditBlogCategory(item);
                return continueEditing ? RedirectToAction("Edit", "BlogCategory", new { Id = item.Id })
                    : RedirectToAction("Index", "BlogCategory");
            }
            else
            {
                var listCategory = _blogCategoryService.GetBlogCategories().ToSelectListItems(-1);
                obj.ListCategory = listCategory;
                return View("Edit", obj);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            _blogCategoryService.DeleteBlogCategory(id);
            return RedirectToAction("Index", "BlogCategory");
        }

    }
}