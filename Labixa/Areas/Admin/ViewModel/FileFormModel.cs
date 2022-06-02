using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Labixa.Areas.Admin.ViewModel
{
    [FluentValidation.Attributes.Validator(typeof(FileFormModel))]
    public class FileFormModel
    {
        [Key]
        public int Id { get; set; }
        [DisplayName(@"Tên")]
        public string Name { get; set; }
        [DisplayName(@"SDT")]
        public string Phone { get; set; }
        [DisplayName(@"Tên blog")]
        public string BlogName { get; set; }
        [DisplayName(@"BlogId")]
        public int BlogId { get; set; }
        [DisplayName(@"Email")]
        public string Email { get; set; }
    }
    public class FileValidator : AbstractValidator<FileFormModel>
    {
        public FileValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Tên Không Được Để Trống");
            RuleFor(x => x.Phone).NotNull().WithMessage("Số điện thoại Không Được Để Trống");
            RuleFor(x => x.BlogName).NotNull().WithMessage("Tên blog Không Được Để Trống");
            RuleFor(x => x.Email).NotNull().WithMessage("Email Không Được Để Trống");
        }
    }
}