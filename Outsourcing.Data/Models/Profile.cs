using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class Profile:BaseEntity
    {
        public Profile()
        {
            PetProfiles = new HashSet<PetProfile>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DayOfBirth { get; set; }
        public string Address { get; set; }
        public int? Gender { get; set; }
        public int? MemberCardId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Deleted { get; set; }
        public string ConfirmToken { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool? Actived { get; set; }
        public int? Role { get; set; }
        public string Image { get; set; }
        public string TokenChangePass { get; set; }

        public virtual ICollection<PetProfile> PetProfiles { get; set; }
    }
}
