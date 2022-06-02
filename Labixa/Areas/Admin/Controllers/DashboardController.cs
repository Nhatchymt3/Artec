using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Outsourcing.Data;
using Outsourcing.Service;

namespace Labixa.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin, Manager, Employee")]
    public class DashboardController : Controller
    {
        #region Field
        IBlogService _blogService;
        #endregion

        public DashboardController(IBlogService blogService)
        {
            this._blogService = blogService;
        }
        //
        // GET: /Admin/Dashboard/
        public ActionResult Index()
        {
            var test = _blogService.GetBlogs();
            return View();
        }

        public ActionResult LoadWebsiteAttribute()
        {

            return PartialView();
        }
	}
}