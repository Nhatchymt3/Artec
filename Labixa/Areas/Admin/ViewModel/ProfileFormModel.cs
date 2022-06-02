using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Labixa.Areas.Admin.ViewModel
{
    [FluentValidation.Attributes.Validator(typeof(ProfileFormModel))]
    public class ProfileFormModel
    {
        [Key]
        public int? Id { get; set; }
        [DisplayName(@"Tên")]
        public string FirstName { get; set; }
        [DisplayName(@"Họ")]
        public string LastName { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName(@"Ngày sinh")]
        public DateTime? DayOfBirth { get; set; }
        [DisplayName(@"Địa chỉ")]
        public string Address { get; set; }
        [DisplayName(@"Giới tính")]
        public int? Gender { get; set; }
        [DisplayName(@"Số Điện Thoại")]
        public string PhoneNumber { get; set; }
        [DisplayName(@"Email")]
        public string Email { get; set; }
        [DisplayName(@"Mật Khẩu")]
        public string Password { get; set; }
        [DisplayName(@"MemberShip")]
        public int? MemberCardId { get; set; }
        public bool? Deleted { get; set; }
        public string Image { get; set; }
    }
    public class ProfileValidator : AbstractValidator<ProfileFormModel>
    {
        public ProfileValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("Họ Không Được Để Trống");
            RuleFor(x => x.LastName).NotNull().WithMessage("Têm Không Được Để Trống");
            RuleFor(x => x.DayOfBirth).NotNull().WithMessage("Ngày sinh Không Được Để Trống");
            RuleFor(x => x.Address).NotNull().WithMessage("Địa chỉ Không Được Để Trống");
            RuleFor(x => x.Gender).NotNull().WithMessage("Giới tính Không Được Để Trống");
            RuleFor(x => x.PhoneNumber).NotNull().WithMessage("Điện Thoại Không Được Để Trống");
            RuleFor(x => x.Email).NotNull().WithMessage("Email Không Được Để Trống");
            RuleFor(x => x.Password).NotNull().WithMessage("Mật khẩu Không Được Để Trống");
        }
    }
}