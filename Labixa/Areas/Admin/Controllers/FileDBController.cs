using Outsourcing.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Manager, Employee")]
    public class FileDBController : Controller
    {
        #region Field
        readonly IFileService _fileService;
        #endregion

        #region Ctor
        public FileDBController(IFileService fileService)
        {
            this._fileService = fileService;
        }
        #endregion
        //
        // GET: /Admin/FileDB/
        public ActionResult Index()
        {
            var list = _fileService.GetFiles();
            return View(list);
        }

        public ActionResult Delete(int fileId)
        {
            _fileService.DeleteFile(fileId);
            return RedirectToAction("Index", "FileDB");
        }
    }
}