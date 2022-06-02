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
using Labixa.Helpers;

namespace Labixa.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Manager, Employee")]
    public partial class BlogController : BaseController
    {
        #region Field

        readonly IBlogService _blogService;
        readonly IBlogCategoryService _blogCategoryService;
        readonly ITagService _tagService;
        readonly IBlogTagMappingService _blogTagMappingService;
        #endregion

        #region Ctor
        public BlogController(IBlogService blogService, IBlogCategoryService blogCategoryService, ITagService tagService, IBlogTagMappingService blogTagMappingService)
        {
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
            _tagService = tagService;
            _blogTagMappingService = blogTagMappingService;
        }
        #endregion

        public ActionResult Index()
        {
            var blogs = _blogService.GetBlogs().Where(s => s.Deleted == false);
            return View(model: blogs);
        }
        public ActionResult Infomation()
        {
            var blogs = _blogService.GetBlogs().Where(s => s.BlogCategoryId == 7);
            return View(model: blogs);
        }
        public ActionResult ManageStaticPage()
        {
            var blogs = _blogService.GetStaticPage();
            return View(model: blogs);
        }
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Create()
        {
            List<string> selections = new List<string>();
            selections.Add(@"Kiến thức");
            selections.Add(@"Chuyên môn");
            selections.Add(@"Sáng tạo");
            selections.Add(@"Thông dụng");
            selections.Add(@"tag1");
            selections.Add(@"tag1");
            ViewBag.selections = selections;
            //Get the list category
            var listCategory = _blogCategoryService.GetBlogCategories().Where(s => s.Id == 3 || s.Id == 4 || s.Id == 2).ToSelectListItems(-1);
            var listTagsBlog = _tagService.GetTags().Where(p => p.IsBlog).ToSelectListItems(-1);
            var listBlogParent = _tagService.GetTags().Where(p => p.IsBlog).ToSelectListItems(-1);
            var blog = new BlogFormModel { ListCategory = listCategory, ListBlogParent = listBlogParent };
            return View(blog);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Create(BlogFormModel newBlog, bool continueEditing, string hfSelected, string hfSelected1)
        {
            if (ModelState.IsValid)
            {

                //Mapping to domain
                Blog blog = Mapper.Map<BlogFormModel, Blog>(newBlog);
                if (String.IsNullOrEmpty(blog.Slug))
                {
                    blog.Slug = StringConvert.ConvertShortName(blog.Title);
                }
                //Create Blog
                blog.BlogParentId = hfSelected1;
                _blogService.CreateBlog(blog);
                var test = blog.Id;
                //if (!string.IsNullOrEmpty(hfSelected))
                //{
                //    string[] arrSplit = hfSelected.Split(',');
                //    foreach (var item in arrSplit)
                //    {
                //        _blogTagMappingService.CreateBlogTagMapping(new BlogTagMapping
                //        {
                //            BlogId = blog.Id,
                //            TagId = int.Parse(item)
                //        });
                //    }

                //}
                return continueEditing ? RedirectToAction("Edit", "Blog", new { blogId = blog.Id, blogParentId = blog.BlogParentId })
                                  : RedirectToAction("Index", "Blog");
            }
            else
            {
                List<string> selections = new List<string>();
                selections.Add(@"Kiến thức");
                selections.Add(@"Chuyên môn");
                selections.Add(@"Sáng tạo");
                selections.Add(@"Thông dụng");
                selections.Add(@"tag1");
                selections.Add(@"tag1");
                ViewBag.selections = selections;
                var listTagsBlog = _tagService.GetTags().Where(p => p.IsBlog).ToSelectListItems(-1);
                var listBlogParent = _tagService.GetTags().Where(p => p.IsBlog).ToSelectListItems(-1);
                newBlog.ListCategory = _blogCategoryService.GetBlogCategories().ToSelectListItems(-1);
                //newBlog.ListTagBlog = listTagsBlog;
                newBlog.ListBlogParent = listBlogParent;
                return View("Create", newBlog);
            }
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public ActionResult Edit(int blogId, string blogParent)
        {

            var blog = _blogService.GetBlogById(blogId);
            BlogFormModel blogFormModel = Mapper.Map<Blog, BlogFormModel>(blog);
            blogFormModel.ListCategory = _blogCategoryService.GetBlogCategories().Where(s => s.Id == 3 || s.Id == 4 || s.Id == 2).ToSelectListItems(blog.BlogCategoryId);
            //blogFormModel.ListTagBlog = _tagService.GetTags().Where(p => p.IsBlog).ToSelectListItems(-1);
            //ViewBag.listTagBlogMap = _blogTagMappingService.GetBlogTagMappings().Where(p => p.BlogId == blogId && p.Tag.IsBlog).ToList();
            blogFormModel.ListBlogParent = _blogService.GetBlogs().Where(p => p.Id != blogId).ToSelectListItems(-1);
            var listSelectedBlogParent = new List<Blog>();
            //if (blogParent != null && blogParent != "")
            //{

            //    foreach (var item in blogParent.Split(',').ToList())
            //    {
            //        listSelectedBlogParent.Add(_blogService.GetBlogs().Where(p => p.Id == int.Parse(item)).FirstOrDefault());
            //    }
            //}
            listSelectedBlogParent.Add(blog);
            //listSelectedBlogParent.Add(blog);
            ViewBag.listBlogParent = listSelectedBlogParent;

            return View(model: blogFormModel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(BlogFormModel blogToEdit, bool continueEditing, string hfSelected1)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                Blog blog = Mapper.Map<BlogFormModel, Blog>(blogToEdit);
                if (String.IsNullOrEmpty(blog.Slug))
                {
                    blog.Slug = StringConvert.ConvertShortName(blog.Title);
                }
                blog.BlogParentId = hfSelected1;
                _blogService.EditBlog(blog);
                //blogToEdit.ListBlogParent = _blogService.GetBlogs().Where(p => p.Id != blog.Id).ToSelectListItems(-1);
                //ViewBag.listBlogParent = _blogService.GetBlogs().Where(p => p.Id == blog.Id && p.BlogParentId == blog.BlogParentId).ToList();


                //if (!string.IsNullOrEmpty(hfSelected))
                //{
                //    string[] arrSpllit = hfSelected.Split(',');
                //    foreach (var item in arrSpllit)
                //    {
                //        var check = _blogTagMappingService.GetBlogTagMappings().Where(p => p.BlogId == blogToEdit.Id && p.TagId == int.Parse(item) && p.Tag.IsBlog).ToList();
                //        if(check.Count() == 0)
                //        {
                //            _blogTagMappingService.CreateBlogTagMapping(new BlogTagMapping {
                //                BlogId = blog.Id,
                //                TagId = int.Parse(item)
                //            });
                //        }
                //    }
                //}
                return continueEditing ? RedirectToAction("Edit", "Blog", new { blogId = blog.Id})
                                 : RedirectToAction("Index", "Blog");
            }
            else
            {
                blogToEdit.ListCategory = _blogCategoryService.GetBlogCategories().Where(s => s.Id == 7 || s.Id == 17 || s.Id == 18 || s.Id == 19 || s.Id == 20).ToSelectListItems(blogToEdit.BlogCategoryId);
                //blogToEdit.ListBlogParent = _blogService.GetBlogs().Where(p => p.Id != blog.Id).ToSelectListItems(-1);
                //ViewBag.listBlogParent = _blogService.GetBlogs().Where(p => p.Id == blog.Id && p.BlogParentId == blog.BlogParentId).ToList();
                return View("Edit", blogToEdit);
            }
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public ActionResult EditStaticPage(int blogId, int cateId, string blogParent)
        {
            var blog = _blogService.GetBlogById(blogId);
            BlogFormModel blogFormModel = Mapper.Map<Blog, BlogFormModel>(blog);
            blogFormModel.BlogCategoryId = cateId;
            blogFormModel.ListBlogParent = _blogService.GetBlogs().Where(p => p.Id != blogId).ToSelectListItems(-1);
            var listSelectedBlogParent = new List<Blog>();
            listSelectedBlogParent.Add(blog);
            ViewBag.listBlogParent = listSelectedBlogParent;
            //blogFormModel.ListBlogParent = _blogService.GetBlogs().Where(p => p.Id != blogId).ToSelectListItems(-1);
            //ViewBag.listBlogParent = _blogService.GetBlogs().Where(p => p.Id == blogId && p.BlogParentId == blogParent).ToList();
            return View(model: blogFormModel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult EditStaticPage(BlogFormModel blogToEdit, bool continueEditing, string hfSelected1)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                Blog blog = Mapper.Map<BlogFormModel, Blog>(blogToEdit);
                //blog.Slug = StringConvert.ConvertShortName(blogToEdit.Title);
                if (String.IsNullOrEmpty(blog.Slug))
                {
                    blog.Slug = StringConvert.ConvertShortName(blog.Title);
                }
                blog.BlogParentId = hfSelected1;
                //blog.BlogCategoryId = 2;
                _blogService.EditBlog(blog);
                return continueEditing ? RedirectToAction("EditStaticPage", "Blog", new { blogId = blog.Id, cateId = blog.BlogCategoryId, blogParentId = blog.BlogParentId })
                               : RedirectToAction("PageStatic", "Blog", new { id = blogToEdit.BlogCategoryId });
            }
            else
            {
                blogToEdit.ListCategory = _blogCategoryService.GetBlogCategories().ToSelectListItems(-1);
                return View("EditStaticPage", blogToEdit);
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Delete(int blogId)
        {
            _blogService.DeleteBlog(blogId);
            return RedirectToAction("Index");
        }

        #region page static
        public ActionResult PageStatic(int id)
        {
            var model = _blogService.GetBlogs().Where(p => p.BlogCategoryId == id).ToList();
            ViewBag.BlogId = id;
            return View(model);
        }
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreatePageStatic(int id)
        {
            var blog = new BlogFormModel();
            blog.BlogCategoryId = id;
            return View(model: blog);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult CreatePageStatic(BlogFormModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                //Mapping to domain
                Blog blog = Mapper.Map<BlogFormModel, Blog>(model);
                if (String.IsNullOrEmpty(blog.Slug))
                {
                    blog.Slug = StringConvert.ConvertShortName(blog.Title);
                }
                //Create Blog
                _blogService.CreateBlog(blog);
                return continueEditing ? RedirectToAction("Edit", "Blog", new { blogId = blog.Id })
                                  : RedirectToAction("PageStatic", "Blog", new { id = model.BlogCategoryId });
            }
            else
            {
                return View("Create", model);
            }
        }
        #endregion
    }
}