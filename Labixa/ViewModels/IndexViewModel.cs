using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Outsourcing.Data.Models;
namespace Labixa.ViewModels
{
    public class IndexViewModel
    {
        public List<WebsiteAttribute> websiteAttributes { get; set; }

        public ProductCategory productCategory { get; set; }

        public Blog blog { get; set; }

        public IEnumerable<Product> productRelated { get; set; }

        public IEnumerable<Blog> blogRelated { get; set; }

        public IEnumerable<Blog> blogReadMore { get; set; }

        public string keySearch { get; set; }

    }
}