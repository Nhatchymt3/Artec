using System.ComponentModel.DataAnnotations.Schema;

namespace Outsourcing.Data.Models
{
    public class BlogTagMapping : BaseEntity
    {
        public int BlogId { get; set; }
        public int TagId { get; set; }
        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; }
        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
    }
}