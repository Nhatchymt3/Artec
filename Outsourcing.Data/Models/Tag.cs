using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public string Slug { get; set; }
        public bool IsBlog { get; set; }

        public ICollection<BlogTagMapping> BlogTagMappings { get; set; }
    }
}
