using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class ProductPictureMapping : BaseEntity
    {
        public string Title { get; set; }       
        public int DisplayOrder { get; set; }
        public bool IsMainPicture { get; set; }
        public int ProductId { get; set; }
        public int PictureId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Picture Picture { get; set; }

    }
}
