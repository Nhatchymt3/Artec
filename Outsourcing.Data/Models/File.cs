using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class File:BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PathFile { get; set; }
        public string BlogName { get; set; }
        public int BlogId { get; set; }
        public string Email { get; set; }
    }
}
