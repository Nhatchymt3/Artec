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
    public class ProductRelationship :BaseEntity
    {
        public int ProductRelateId { get; set; }
        public int ProductId { get; set; }
        public bool isAvailable { get; set; }

        //[ForeignKey("ProductRelateId")]
        //virtual public Product Product { get; set; }

        //[ForeignKey("ProductId")]
        //virtual public Product Product1 { get; set; }

    }
}
