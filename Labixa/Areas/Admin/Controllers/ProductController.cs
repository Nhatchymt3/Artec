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
using System.IO;

namespace Labixa.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin, Manager, Employee")]
    public partial class ProductController : Controller
    {
        #region Field

        readonly IProductService _productService;
        readonly IProductCategoryService _productCategoryService;

        readonly IProductAttributeService _productAttributeService;
        readonly IProductAttributeMappingService _productAttributeMappingService;

        readonly IPictureService _pictureService;
        readonly IProductPictureMappingService _productPictureMappingService;


        readonly IProductRelationshipService _productRelationShipService;
        readonly ITagService _tagService;
        readonly IProductTagMappingService _productTagMappingService;
        #endregion

        #region Ctor
        public ProductController(IProductService productService, IProductCategoryService productCategoryService,
            IProductAttributeService productAttributeService, IProductAttributeMappingService productAttributeMappingService,
            IPictureService pictureService, IProductPictureMappingService productPictureMappingService,
            IProductRelationshipService productRelationShipService, ITagService tagService, IProductTagMappingService productTagMappingService)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
            this._productAttributeService = productAttributeService;
            this._productAttributeMappingService = productAttributeMappingService;
            this._pictureService = pictureService;
            this._productPictureMappingService = productPictureMappingService;
            this._productRelationShipService = productRelationShipService;
            this._tagService = tagService;
            this._productTagMappingService = productTagMappingService;
        }
        #endregion
        //void adddata(Product item)
        //{

        //    ProductAttributeMapping obj = new ProductAttributeMapping();
        //    obj.IsRequired = false;
        //    obj.ProductAttributeId = 13;
        //    obj.ProductId = item.Id;
        //    obj.Value = "true";
        //    _productAttributeMappingService.CreateProductAttributeMapping(obj);
        //}
        public ActionResult Index()
        {
            var products = _productService.GetProducts().ToList();
            //foreach (var item in products)
            //{
            //    adddata(item);
            //}
            //foreach (var item in products)
            //{
            //    ProductAttributeMapping obj = new ProductAttributeMapping();
            //    obj.IsRequired = false;
            //    obj.Value = "true";
            //    obj.DisplayOrder = 0;
            //    obj.ProductId = item.Id;
            //    obj.ProductAttributeId = 13;
            //    _productAttributeMappingService.CreateProductAttributeMapping(obj);
            ////adddata(item.Id);
            //}
            return View(model: products);
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create()
        {
            //Get the list category
            var listProductCategory = _productCategoryService.GetProductCategories().ToSelectListItems(-1);
            var listTagsProduct = _tagService.GetTags().Where(p => !p.IsBlog).ToSelectListItems(-1);
            var product = new ProductFormModel { ListProductCategory = listProductCategory, ListTagsProduct = listTagsProduct };
            return View(product);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Create(ProductFormModel newProduct, bool continueEditing, string hfSelected)
        {
            //newProduct.FileUpload.SaveAs("~/Areas/Admin/Models");
            newProduct.FileUpload.SaveAs(Server.MapPath($"~/Areas/Admin/Models/{newProduct.FileUpload.FileName}"));
            newProduct.DescEng = newProduct.FileUpload.FileName;
            newProduct.Tags = "/Areas/Admin/Models/" + newProduct.FileUpload.FileName;
            if (ModelState.IsValid)
            {
                //Mapping to domain
                Product product = Mapper.Map<ProductFormModel, Product>(newProduct);
                if (String.IsNullOrEmpty(product.Slug))
                {
                    product.Slug = StringConvert.ConvertShortName(product.Name);
                }

                //Create Product
                _productService.CreateProduct(product);
                if (!string.IsNullOrEmpty(hfSelected))
                {
                    string[] arrSplit = hfSelected.Split(',');
                    foreach (var item in arrSplit)
                    {
                        _productTagMappingService.CreateProductTagMapping(new ProductTagMapping
                        {
                            ProductId = product.Id,
                            TagId = int.Parse(item)
                        });
                    }
                }

                ////Add ProductAttribute after product created
                //product.ProductAttributeMappings = new Collection<ProductAttributeMapping>();
                //var listAttributeId = _productAttributeService.GetProductAttributes().Select(p => p.Id);
                //foreach (var id in listAttributeId)
                //{
                //    product.ProductAttributeMappings.Add(
                //        new ProductAttributeMapping() { ProductAttributeId = id, ProductId = product.Id, Price = 0, OldPrice = 0 });

                //}

                //Add Picture default for Labixa
                product.ProductPictureMappings = new Collection<ProductPictureMapping>();
                for (int i = 0; i < 4; i++)
                {
                    var newPic = new Picture();
                    bool ismain = i == 0;
                    _pictureService.CreatePicture(newPic);
                    product.ProductPictureMappings.Add(
                        new ProductPictureMapping()
                        {
                            PictureId = newPic.Id,
                            ProductId = product.Id,
                            IsMainPicture = ismain,
                            DisplayOrder = 0,

                        });
                }
                _productService.EditProduct(product);


                //create product relation
                #region
                ProductRelationship item2 = new ProductRelationship();
                for (int i = 0; i < 3; i++)
                {
                    ProductRelationship item = new ProductRelationship();
                    item.ProductId = product.Id;
                    item.ProductRelateId = product.Id;
                    item.isAvailable = true;
                    _productRelationShipService.CreateProductRelationship(item);
                }
                #endregion
                //Save all after edit
                return continueEditing ? RedirectToAction("Edit", "Product", new { productId = product.Id })
                                : RedirectToAction("Index", "Product");
            }
            else
            {
                var listProductCategory = _productCategoryService.GetProductCategories().ToSelectListItems(-1);
                newProduct.ListProductCategory = listProductCategory;
                return View("Create", newProduct);
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public ActionResult Edit(int productId)
        {

            var product = _productService.GetProductById(productId);
            ProductFormModel productFormModel = Mapper.Map<Product, ProductFormModel>(product);
            productFormModel.ListProductCategory = _productCategoryService.GetProductCategories().ToSelectListItems(-1);
            productFormModel.ListProducts = _productService.GetAllProducts().ToSelectListItems(-1);
            var list = _productRelationShipService.GetProductById(productId);
            productFormModel.Id1 = list.ToList()[0].ProductRelateId;
            productFormModel.Id2 = list.ToList()[1].ProductRelateId;
            productFormModel.Id3 = list.ToList()[2].ProductRelateId;
            productFormModel.ListTagsProduct = _tagService.GetTags().Where(p => !p.IsBlog).ToSelectListItems(-1);
            ViewBag.listTagProductMap = _productTagMappingService.GetProductTagMappings().Where(p => p.ProductId == productId).ToList();

            productFormModel.ListProductAttributeMapping = _productAttributeMappingService.GetProductAttributeMappings().Where(p => p.ProductId == productId).ToList();

            return View(model: productFormModel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(ProductFormModel productToEdit, bool continueEditing, string hfSelected, FormCollection dataForm)
        {
            if (ModelState.IsValid)
            {
                if (productToEdit.FileUpload != null)
                {
                    productToEdit.FileUpload.SaveAs(Server.MapPath($"~/Areas/Admin/Models/{productToEdit.FileUpload.FileName}"));
                    productToEdit.DescEng = productToEdit.FileUpload.FileName;
                    productToEdit.Tags = "/Areas/Admin/Models/" + productToEdit.FileUpload.FileName;
                }

                //Mapping to domain 
                Product product = Mapper.Map<ProductFormModel, Product>(productToEdit);
                if (String.IsNullOrEmpty(product.Slug))
                {
                    product.Slug = StringConvert.ConvertShortName(product.Name);
                }
                //this funcion not update all relationship value.                           
                _productService.EditProduct(product);
                if (!string.IsNullOrEmpty(hfSelected))
                {
                    string[] arrSpllit = hfSelected.Split(',');
                    foreach (var item in arrSpllit)
                    {
                        var check = _productTagMappingService.GetProductTagMappings().Where(p => p.ProductId == productToEdit.Id && p.TagId == int.Parse(item)).ToList();
                        if (check.Count() == 0)
                        {
                            _productTagMappingService.CreateProductTagMapping(new ProductTagMapping
                            {
                                ProductId = product.Id,
                                TagId = int.Parse(item)
                            });
                        }
                    }
                }
                //update attribute
                if (product.ProductAttributeMappings != null && product.ProductAttributeMappings.Count > 0)
                {
                    foreach (var mapping in product.ProductAttributeMappings)
                    {
                        _productAttributeMappingService.EditProductAttributeMapping(mapping);
                    }
                }
                ////update productpicture URL
                foreach (var picture in product.ProductPictureMappings)
                {
                    _productPictureMappingService.EditProductPictureMapping(picture);
                    _pictureService.EditPicture(picture.Picture);
                }

                var firstImage = product.ProductPictureMappings.ToList().FirstOrDefault();
                if (firstImage != null)
                {
                    product.ProductImage = firstImage.Picture.Url;
                    _productService.EditProduct(product);
                }
                //add tour relation
                #region [add tour relation]
                var listProductRelation = _productRelationShipService.GetProductById(product.Id);
                var item1 = listProductRelation.ToList()[0];
                var item2 = listProductRelation.ToList()[1];
                var item3 = listProductRelation.ToList()[2];
                item1.ProductRelateId = productToEdit.Id1;
                item2.ProductRelateId = productToEdit.Id2;
                item3.ProductRelateId = productToEdit.Id3;
                _productRelationShipService.EditProductRelationship(item1);
                _productRelationShipService.EditProductRelationship(item2);
                _productRelationShipService.EditProductRelationship(item3);
                #endregion
                return continueEditing ? RedirectToAction("Edit", "Product", new { productId = product.Id })
                      : RedirectToAction("Index", "Product");
            }
            else
            {
                var listProductCategory = _productCategoryService.GetProductCategories().ToSelectListItems(-1);
                productToEdit.ListProductCategory = listProductCategory;
                return View("Edit", productToEdit);
            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int productId)
        {
            _productService.DeleteProduct(productId);
            return RedirectToAction("Index");
        }

        public ActionResult AttributeProduct()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddAttributeProduct(string idproduct, string attributeName, string attributePrice, string attributeDes)
        {
            try
            {
                var attributeObject = new ProductAttribute()
                {
                    Name = attributeName,
                    Description = attributeDes,
                    ControlType = "Text",
                    Deleted = false
                };
                _productAttributeService.CreateProductAttribute(attributeObject);
                var attributeMapping = new ProductAttributeMapping()
                {
                    Value = attributeName,
                    IsRequired = false,
                    DisplayOrder = 0,
                    ProductId = int.Parse(idproduct),
                    ProductAttributeId = attributeObject.Id,
                    Price = int.Parse(attributePrice)
                };
                _productAttributeMappingService.CreateProductAttributeMapping(attributeMapping);
                return Json(new { code = 1, message = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { code = 2, message = "Opps" }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult deleteAttribute(int attri,int prodid)
        {
            _productAttributeService.DeleteProductAttributeCascade(attri);
            return RedirectToAction("Edit", new { productId = prodid });
        }
    }
}