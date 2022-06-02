using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Outsourcing.Data.Models;
using FluentValidation.Mvc;
using FluentValidation.Validators;
using FluentValidation;

namespace Labixa.Areas.Admin.ViewModel
{
    [FluentValidation.Attributes.Validator(typeof(ProductValidator))]

    public class ProductFormModel
    {
        public ProductFormModel()
        {
            ListProductCategory = new List<SelectListItem>();
            ListProducts = new List<SelectListItem>();
            //ListTagsProduct = new List<SelectListItem>();
            ListProductAttributeMapping = new List<ProductAttributeMapping>();
        }

        [Key]
        public int Id { get; set; }

        [DisplayName(@"Tên File")]
        public string Name { get; set; }
        [DisplayName(@"Ngày tạo")]
        public DateTimeOffset DayCreate { get; set; }


        [DisplayName(@"Người up load")]
        public string Description { get; set; }

        [DisplayName(@"Nội dung")]
        public string Content { get; set; }

        [DisplayName(@"Hiển thị trên trang chủ")]
        public bool IsHomePage { get; set; }

        [DisplayName(@"Công bố")]
        public bool IsPublic { get; set; }

        [DisplayName(@"Giá hiện tại của sản phẩm")]
        public int Price { get; set; }
        [DisplayName(@"Giá cũ sản phẩm")]
        public int OldPrice { get; set; }
        [DisplayName(@"File Gốc")]
        public string NameEng { get; set; }
        [DisplayName(@"File PDF")]
        public string DescEng { get; set; }
        [DisplayName(@"Dung lượng File")]
    
        public HttpPostedFileWrapper FileUpload { get; set; }

        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public string ContentEng { get; set; }
        /// <summary>
        /// URL  SEO friendly
        /// </summary>
        [DisplayName(@"Đường dẫn")]
        public string Slug { get; set; }
        [DisplayName(@"Thẻ meta từ khóa")]
        public string MetaKeywords { get; set; }

        [DisplayName(@"Thẻ meta Trang")]
        public string MetaTitle { get; set; }

        [DisplayName(@"Thẻ meta Mô tả")]
        public string MetaDescription { get; set; }
        [DisplayName(@"Danh mục")]
        public int ProductCategoryId { get; set; }

        [DisplayName(@"Danh mục")]
        public IEnumerable<SelectListItem> ListProductCategory { get; set; }

        [DisplayName(@"Thuộc tính sản phẩm")]
        public ICollection<ProductAttributeMapping> ProductAttributeMappings { get; set; }

        [DisplayName(@"Hình ảnh sản phẩm")]
        public ICollection<ProductPictureMapping> ProductPictureMappings { get; set; }

        [DisplayName(@"Sản Phẩm")]
        public IEnumerable<SelectListItem> ListProducts { get; set; }
        [DisplayName(@"Tags")]
        public IEnumerable<SelectListItem> ListTagsProduct { get; set; }

        [DisplayName(@"Vị trí đặt")]
        public int Position { get; set; }

        [DisplayName(@"Tour Liên Quan 1 : ")]
        public int Id1 { get; set; }

        [DisplayName(@"Tour Liên Quan 2 : ")]
        public int Id2 { get; set; }

        [DisplayName(@"Tour Liên Quan 3 : ")]
        public int Id3 { get; set; }

        [DisplayName(@"Trữ hàng : ")]
        public int? Stock { get; set; }
        [DisplayName(@"Tags : ")]
        public string Tags { get; set; }
        [DisplayName(@"Người thêm : ")]
        public string UserCreate { get; set; }
        [DisplayName(@"Số lượt thích")]
        public int? LikeProduct { get; set; }

        public string ProductImage { get; set; }

        public int AttributePrice { get; set; }
        public string AttributeName { get; set; }
        public List<ProductAttributeMapping> ListProductAttributeMapping { get; set; }
    }
    public class ProductValidator : AbstractValidator<ProductFormModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Tên Không Được Để Trống");
           
            //RuleFor(x => x.Description).NotNull().WithMessage("Mô Tả Không Được Để Trống");
            //RuleFor(x => x.Price).NotNull().WithMessage("Giá Không Được Để Trống");
        }
    }
}