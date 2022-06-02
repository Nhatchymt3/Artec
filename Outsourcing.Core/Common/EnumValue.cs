using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Core.Common
{
    public class EnumValue
    {
        public enum Gender:int
        {
            [Display(Name = "Nam")]
            Male = 0,
            [Display(Name = "Nữ")]
            Female = 1,
        }
        public enum MemberShip:int
        {
            [Display(Name = @"Bạc")]
            Silver = 0,
            [Display(Name = @"Vàng")]
            Gold = 1,
            [Display(Name = @"Bạch kim")]
            Platinum = 2,
            [Display(Name = @"Kim cương")]
            Diamond = 3,
        }
        public enum PetStatus : int
        {
            [Display(Name = @"Khỏe mạnh")]
            Heathy = 0,
            [Display(Name = @"Yếu")]
            Weak = 1,
        }
    }
}
