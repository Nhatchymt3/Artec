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
    public class Staff :BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Rename { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public bool Deleted { get; set; }
        public string Yahoo { get; set; }
        public string skype { get; set; }
    }
}
