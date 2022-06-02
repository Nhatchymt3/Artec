using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class Location : BaseEntity
    {
        public string City { get; set; }
        public string Address { get; set; }
        public bool Deleted { get; set; }
        public string Temp_1 { get; set; }
        public string Temp_2 { get; set; }
        public string Temp_3 { get; set; }
        public string Temp_4 { get; set; }
        public string Temp_5 { get; set; }
        public string Temp_6 { get; set; }
        public string Temp_7 { get; set; }
        public string Temp_8 { get; set; }
        public string Temp_9 { get; set; }
        public string Temp_10 { get; set; }

        virtual public ICollection<Recruit> Recuits { get; set; }

    }
}
