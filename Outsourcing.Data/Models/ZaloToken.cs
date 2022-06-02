using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class ZaloToken : BaseEntity
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string expires_in { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
