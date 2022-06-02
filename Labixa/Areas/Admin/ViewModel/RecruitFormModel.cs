
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Labixa.Areas.Admin.ViewModel
{
    [FluentValidation.Attributes.Validator(typeof(RecruitValidator))]
    public class RecruitFormModel
    {
        [Key]
        public int? Id { get; set; }
        [DisplayName(@"Tiêu đề")]
        public string Title { get; set; }
        [DisplayName(@"Đường dẫn")]
        public string Temp_1 { get; set; }
        [DisplayName(@"Hình Ảnh")]
        public string Temp_2 { get; set; }
        [DisplayName(@"Mô tả")]
        public string Description { get; set; }
        [DisplayName(@"Nội dung")]
        public string Content { get; set; }
        [DisplayName(@"Ngày bắt đầu")]
        public DateTime DayStart { get; set; }
        [DisplayName(@"Ngày kết thúc")]
        public DateTime DayEnd { get; set; }
        [DisplayName(@"Phòng ban")]
        public int DepartmentId { get; set; }
        [DisplayName(@"Địa điểm")]
        public int LocationId { get; set; }
        public bool? Deleted { get; set; }
    }
    public class RecruitValidator : AbstractValidator<RecruitFormModel>
    {
        public RecruitValidator()
        {
            RuleFor(x => x.Title).NotNull().WithMessage("Tiêu đề không được để trống");
            RuleFor(x => x.Description).NotNull().WithMessage("Mô tả không được để trống");
            //RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả không được để trống");
            RuleFor(x => x.Content).NotNull().WithMessage("Nội dung không được để trống");
            RuleFor(x => x.DayStart).NotNull().WithMessage("Ngày bắt đầu không được để trống");
            RuleFor(x => x.DayEnd).NotNull().WithMessage("Ngày kết thúc không được để trống");
            RuleFor(x => x.DayStart).LessThan(x=> x.DayEnd).WithMessage("Ngày kết thúc phải lớn hơn ngày bắt đầu");
        }
    }
}