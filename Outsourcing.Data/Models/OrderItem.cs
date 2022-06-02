using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public  class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public int Price { get; set; }
        public int? AttributeId { get; set; }
        public string Discount { get; set; }

        public string AttributeName { get; set; }
        public string Temp_1 { get; set; }
        public string Temp_2 { get; set; }
        public string Temp_3 { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
