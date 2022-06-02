using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class ProductTagMapping:BaseEntity
    {
        public int ProductId { get; set; }
        public int TagId { get; set; }
        //[ForeignKey("ProductId")]
        //public virtual Product Product { get; set; }
        //[ForeignKey("TagId")]
        //public virtual Tag Tag { get; set; }
    }
}
