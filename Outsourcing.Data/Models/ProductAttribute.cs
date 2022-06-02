using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class ProductAttribute : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ControlType { get; set; }

        public virtual ICollection<ProductAttributeMapping> ProductAttributes { get; set; }

        public bool Deleted { get; set; }
        public string Image { get; set; }
    }
}
