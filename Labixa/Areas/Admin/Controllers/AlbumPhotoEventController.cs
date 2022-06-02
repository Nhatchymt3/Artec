using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class AlbumPhotoEventController : Controller
    {
        #region Field

        readonly IProductService _productService;
        readonly IProductCategoryService _productCategoryService;

        readonly IProductAttributeService _productAttributeService;
        readonly IProductAttributeMappingService _productAttributeMappingService;

        readonly IPictureService _pictureService;
        readonly IProductPictureMappingService _productPictureMappingService;


        readonly IProductRelationshipService _productRelationShipService;

        #endregion

        #region Ctor
        public AlbumPhotoEventController(IProductService productService, IProductCategoryService productCategoryService,
            IProductAttributeService productAttributeService, IProductAttributeMappingService productAttributeMappingService,
            IPictureService pictureService, IProductPictureMappingService productPictureMappingService,
            IProductRelationshipService productRelationShipService )
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
            this._productAttributeService = productAttributeService;
            this._productAttributeMappingService = productAttributeMappingService;
            this._pictureService = pictureService;
            this._productPictureMappingService = productPictureMappingService;
            this._productRelationShipService = productRelationShipService;
        }
        #endregion

        //
        // GET: /Admin/AlbumPhoto/
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(AlbumPhotoFormModel photoModel)
        {
            Product product = Mapper.Map<AlbumPhotoFormModel, Product>(photoModel);
            foreach (var picture in product.ProductPictureMappings)
            {
                _productPictureMappingService.EditProductPictureMapping(picture);
                _pictureService.EditPicture(picture.Picture);
            }
            _productService.EditProduct(product);
            return RedirectToAction("Index","AlbumPhotoEvent");
        }
        //public ActionResult Create()
        //{
        //    Picture pic = new Picture();
        //    _pictureService.CreatePicture(pic);
        //    ProductPictureMapping pictureMapping = new ProductPictureMapping();
        //    pictureMapping.DisplayOrder = 0;
        //    pictureMapping.IsMainPicture = false;
        //    pictureMapping.PictureId = pic.Id;
        //    pictureMapping.ProductId = 50;
        //    _productPictureMappingService.CreateProductPictureMapping(pictureMapping);
        //    var product = _productService.GetPhoto();
        //    Product photo = new Product();
        //    foreach (var picture in product.ProductPictureMappings)
        //    {
        //        if (picture.Picture.Url==null)
        //        {
        //            photo.ProductPictureMappings.Add(picture);
        //        }
        //    }
        //    return View(model:photo);
        //}

        [HttpPost]
        public  ActionResult InsertPicture()
        {
            Picture picture = new Picture();
            _pictureService.CreatePicture(picture);
            ProductPictureMapping pictureMapping = new ProductPictureMapping();
            pictureMapping.DisplayOrder = 0;
            pictureMapping.IsMainPicture = false;
            pictureMapping.PictureId = picture.Id;
            pictureMapping.ProductId = 52;
            _productPictureMappingService.CreateProductPictureMapping(pictureMapping);
            Product product = _productService.GetPhoto();
            AlbumPhotoFormModel photo = Mapper.Map<Product, AlbumPhotoFormModel>(product);
            return PartialView("_NewPhotoEvent",photo);
        }
	}
}