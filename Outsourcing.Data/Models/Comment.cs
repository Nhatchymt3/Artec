using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class Comment:BaseEntity
    {
        public int BlogId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DateCreate { get; set; }
        public int? CommentID { get; set; }

        public virtual Comment comment { get; set; }
        public virtual Blog Blog { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Profile Profile { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
