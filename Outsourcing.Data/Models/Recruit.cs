using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class Recruit : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime DayStart { get; set; }
        public DateTime DayEnd { get; set; }
        public string Temp_1 { get; set; }
        public string Temp_2 { get; set; }
        public string Temp_3 { get; set; }
        public string Temp_4 { get; set; }
        public string Temp_5 { get; set; }
        public bool Deleted { get; set; }

        public int LocationId { get; set; }
        public int DepartmentId { get; set; }

        [ForeignKey("LocationId")]
        virtual public Location Location { get; set; }
        [ForeignKey("DepartmentId")]
        virtual public Department Department { get; set; }
    }
}
