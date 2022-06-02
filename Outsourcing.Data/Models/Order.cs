using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class Order : BaseEntity
    {
        public Order()
        {
            DateCreated = DateTime.Now;
            Count = 10;
        }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public int OrderTotal { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string RequestId { get; set; }
        public string transId { get; set; }
        public string resultCode { get; set; }
        public string message { get; set; }
        public string payType { get; set; }
        public string responseTime { get; set; }
        public string extraData { get; set; }
        public string signature { get; set; }
        public string partnerCode { get; set; }
        public string ProductName { get; set; }
        public string Product_Slug { get; set; }
        public int? ProfileId { get; set; }
        public string Path_Download { get; set; }
        public int Count { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } 
    }
}
