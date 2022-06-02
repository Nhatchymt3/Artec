using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Labixa.Areas.Admin.ViewModel
{
    [FluentValidation.Attributes.Validator(typeof(LocationValidator))]
    public class LocationFormModel
    {
        [Key]
        public int? Id { get; set; }
        [DisplayName(@"Thành phố")]
        public string City { get; set; }
        [DisplayName(@"Địa chỉ")]
        public string Address { get; set; }
        [DisplayName(@"Mô tả")]
        public bool? Deleted { get; set; }
    }
    public class LocationValidator : AbstractValidator<LocationFormModel>
    {
        public LocationValidator()
        {
            RuleFor(x => x.City).NotNull().WithMessage("Thành phố không được để trống");
            RuleFor(x => x.Address).NotNull().WithMessage("Địa chỉ không được để trống");
        }
    }
}