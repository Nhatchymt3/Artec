using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class MomoModel : BaseEntity
    {
        public string PartnerCode {get;set;}
        public string AccessKey { get;set;}
        public string SecretKey { get;set;}
        public string PublicKey { get;set;}

    }
}
