using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Labixa.Areas.Admin.ViewModel
{
    [FluentValidation.Attributes.Validator(typeof(CommentFormModel))]
    public class CommentFormModel
    {
        [Key]
        public int? Id { get; set; }
        [DisplayName(@"Blog")]
        public int? BlogId { get; set; }
        [DisplayName(@"Người đăng")]
        public int? UserId { get; set; }
        [DisplayName(@"Nội dung")]
        public string Content { get; set; }
        [DisplayName(@"Ngày tạo")]
        public DateTime DateCreate { get; set; }
        [DisplayName(@"Trả lời")]
        public int? CommentID { get; set; }
        public bool? Deleted { get; set; }
    }
    public class CommentValidator : AbstractValidator<CommentFormModel>
    {
        public CommentValidator()
        {
            RuleFor(x => x.BlogId).NotNull().WithMessage("Blog Không Được Để Trống");
            RuleFor(x => x.UserId).NotNull().WithMessage("Têm Không Được Để Trống");
            RuleFor(x => x.Content).NotNull().WithMessage("Nội dung Không Được Để Trống");
            RuleFor(x => x.DateCreate).NotNull().WithMessage("Địa chỉ Không Được Để Trống");
        }
    }
}