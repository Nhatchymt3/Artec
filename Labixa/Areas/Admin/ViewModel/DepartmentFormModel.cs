using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Labixa.Areas.Admin.ViewModel
{
    [FluentValidation.Attributes.Validator(typeof(DepartmentValidator))]
    public class DepartmentFormModel
    {
        [Key]
        public int? Id { get; set; }
        [DisplayName(@"Phòng ban")]
        public string Unit { get; set; }
        [DisplayName(@"Tên")]
        public string Name { get; set; }
        [DisplayName(@"Mô tả")]
        public string Description { get; set; }

        public bool? Deleted { get; set; }
    }
    public class DepartmentValidator : AbstractValidator<DepartmentFormModel>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Unit).NotNull().WithMessage("Phòng ban không được để trống");
            RuleFor(x => x.Name).NotNull().WithMessage("Tên không được để trống");
            RuleFor(x => x.Description).NotNull().WithMessage("Mô tả không được để trống");
        }
    }
}