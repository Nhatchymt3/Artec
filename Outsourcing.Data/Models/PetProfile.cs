using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class PetProfile:BaseEntity
    {
        public string Name { get; set; }
        public DateTime? DayOfBirth { get; set; }
        public int? Gender { get; set; }
        public bool Deleted { get; set; }
        public string Type { get; set; }
        public double? Weight { get; set; }
        public string Size { get; set; }
        public double? Height { get; set; }
        public string Color { get; set; }
        public int? Status { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string Image { get; set; }

        public int? ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}