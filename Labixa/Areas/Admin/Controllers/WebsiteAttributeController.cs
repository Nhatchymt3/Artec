using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Labixa.Areas.Admin.ViewModel;
using Labixa.Areas.Admin.ViewModel.WebsiteAtribute;
using Outsourcing.Core.Framework.Controllers;
using Outsourcing.Data.Models;
using Outsourcing.Service;

namespace Labixa.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WebsiteAttributeController : Controller
    {

        #region Field
        readonly IWebsiteAttributeService _websiteAttributeService;
        #endregion

        #region Ctor
        public WebsiteAttributeController(IWebsiteAttributeService websiteAttributeService)
        {
            _websiteAttributeService = websiteAttributeService;
        }
        #endregion
        //
        // GET: /Admin/WebsiteAttribute/
        public ActionResult Index()
        {
            return RedirectToAction("Manage");
        }
        public ActionResult Manage()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(p => p.Deleted == false).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(WebsiteAttributeFormModel model)
        {
            if (ModelState.IsValid)
            {
                WebsiteAttribute websiteAttribute = Mapper.Map<WebsiteAttributeFormModel, WebsiteAttribute>(model);
                _websiteAttributeService.CreateWebsiteAttribute(websiteAttribute);
                return RedirectToAction("Index", "WebsiteAttribute");
            }
            else
            {
                return View("Create", model);
            }
        }
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int id)
        {
            var websiteAttribute = _websiteAttributeService.GetWebsiteAttributeById(id);
            WebsiteAttributeFormModel model = Mapper.Map<WebsiteAttribute, WebsiteAttributeFormModel>(websiteAttribute);
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("Index", "WebsiteAttribute");
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(WebsiteAttributeFormModel model, bool continueEditing)
        {

            WebsiteAttribute websiteAttribute = Mapper.Map<WebsiteAttributeFormModel, WebsiteAttribute>(model);

            _websiteAttributeService.EditWebsiteAttribute(websiteAttribute);

            return continueEditing ? RedirectToAction("Edit", "WebsiteAttribute", new { id = websiteAttribute.Id })
                                    : RedirectToAction("Index", "WebsiteAttribute");
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult EditAll(List<WebsiteAttributeManageModel> model)
        {
            foreach (var type in model)
            {
                foreach (var attribute in type.WebsiteAttributes)
                {
                    _websiteAttributeService.EditWebsiteAttribute(attribute);
                }
            }
            return RedirectToAction("Manage", "WebsiteAttribute");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            _websiteAttributeService.DeleteWebsiteAttribute(id);
            return RedirectToAction("Index", "WebsiteAttribute");
        }
        public ActionResult HomepageAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("Page_Home")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }

        public ActionResult BannerHomeAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("Banner_Home")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }

        public ActionResult PageStoryAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("Page_Story")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }

        public ActionResult MapAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("Map")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }

        public ActionResult AboutHomeAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("About_Home")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }
        public ActionResult AboutStaffAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("About_Staff")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }

        public ActionResult AboutHospitalAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("About_Hospital")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }
        public ActionResult AboutEquipmentAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("About_Equipment")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }
        public ActionResult AboutTestimonialAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("About_Testimonial")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }
        
        public ActionResult AboutSocialMediaAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("Scocial_Media")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }
        public ActionResult AboutGoogleAnalyticsAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("GoogleAnalytics")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }
        public ActionResult AboutRecruitAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("Recruit")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }
        public ActionResult AboutContactAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("Contact")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }
        public ActionResult AboutBookingAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("Booking")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }
        public ActionResult AboutServicesAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("Services")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }
        public ActionResult AboutKnowledgeAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("Knowledge")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }
        public ActionResult AboutShopAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("Shop")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }
        public ActionResult PetInsuranceAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type.Equals("Pet_Insurance")).ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }

        //--------------------------------------------

        public ActionResult HomeAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type == "Home").ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }

        public ActionResult AboutAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type == "About").ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }

        public ActionResult NewsAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type == "News").ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }

        public ActionResult DocumentAttribute()
        {
            var list = _websiteAttributeService.GetWebsiteAttributes().Where(q => q.Type == "Document").ToList().GroupBy(w => w.Type).Select(w => new WebsiteAttributeManageModel
            {
                Type = w.Key,
                WebsiteAttributes = w.ToList()
            });
            return View(model: list.ToList());
        }
    }

}