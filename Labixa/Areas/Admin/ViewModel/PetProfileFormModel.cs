using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Labixa.Areas.Admin.ViewModel
{
    [FluentValidation.Attributes.Validator(typeof(PetProfileFormModel))]
    public class PetProfileFormModel
    {
        [Key]
        public int Id { get; set; }
        [DisplayName(@"Tên")]
        public string Name { get; set; }
        [DisplayName(@"Tên chủ pet")]
        public int? ProfileId { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName(@"Ngày sinh")]
        public DateTime? DayOfBirth { get; set; }
        [DisplayName(@"Giới tính")]
        public int? Gender { get; set; }
        [DisplayName(@"Chủng loại")]
        public string Type { get; set; }
        [DisplayName(@"Cân nặng")]
        public double? Weight { get; set; }
        [DisplayName(@"Chiều cao")]
        public double? Height { get; set; }
        [DisplayName(@"Size")]
        public string Size { get; set; }
        [DisplayName(@"Màu")]
        public string Color { get; set; }
        [DisplayName(@"Tình trạng sức khỏe")]
        public int? Status { get; set; }
        [DisplayName(@"Kiểm tra gần nhất")]
        public DateTime? LastUpdate { get; set; }
        public bool? Deleted { get; set; }
        public string Image { get; set; }
    }
    public class PetProfileValidator : AbstractValidator<PetProfileFormModel>
    {
        public PetProfileValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Tên Không Được Để Trống");
            RuleFor(x => x.ProfileId).NotNull().WithMessage("Tên chủ pet Không Được Để Trống");
            RuleFor(x => x.DayOfBirth).NotNull().WithMessage("Ngày sinh Không Được Để Trống");
            RuleFor(x => x.Gender).NotNull().WithMessage("Giới tính Không Được Để Trống");
            RuleFor(x => x.Type).NotNull().WithMessage("Chủng loại Không Được Để Trống");
            RuleFor(x => x.Weight).NotNull().WithMessage("Cân nặng Không Được Để Trống");
            RuleFor(x => x.Height).NotNull().WithMessage("Chiều cao Không Được Để Trống");
            RuleFor(x => x.Size).NotNull().WithMessage("Size Không Được Để Trống");
            RuleFor(x => x.Status).NotNull().WithMessage("Tình trạng sức khỏe Không Được Để Trống");
        }
    }
}