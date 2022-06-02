using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Outsourcing.Data.Models
{
    public class Picture :BaseEntity
    {
        public Picture()
        {
            ProductPictureMapping = new HashSet<ProductPictureMapping>();
        }

        public string Binary { get; set; }
        public string MimeType { get; set; }
        public string Url { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<ProductPictureMapping> ProductPictureMapping { get; set; } 
    }
}
