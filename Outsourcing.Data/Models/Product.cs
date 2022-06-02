using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class Product : BaseEntity
    {
        public Product()
        {
            LastEditedTime = DateCreated = DateTime.Now;
        }
        public string Name { get; set; }
        public string Description { get; set; }


        /// <summary>
        /// URL  SEO friendly
        /// </summary>
        public string Slug { get; set; }
        public string Content { get; set; }
        
        public int Price { get; set; }
        public int OldPrice { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string NameEng { get; set; }
        public string DescEng { get; set; }
        public string ContentEng { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastEditedTime { get; set; }
        public int ProductCategoryId { get; set; }
        public bool IsHomePage { get; set; }
        public bool IsPublic { get; set; }
        public bool Deleted { get; set; }
        public int Position { get; set; }
        public int? Stock { get; set; }
        public int? LikeProduct { get; set; }
        public string Tags { get; set; }
        public string ProductImage { get; set; }
        public string UserCreate { get; set; }


        //public int PictureId { get; set; }
        //public virtual Picture Picture { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductPictureMapping> ProductPictureMappings{ get; set; }
        public virtual ICollection<ProductAttributeMapping> ProductAttributeMappings { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
