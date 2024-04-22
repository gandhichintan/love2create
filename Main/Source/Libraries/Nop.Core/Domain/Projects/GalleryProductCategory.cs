using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class GalleryProductCategory : BaseEntity
    {
        public virtual int GalleryProductId { get; set; }

        public virtual int GalleryCategoryId { get; set; }

        public virtual int DisplayOrder { get; set; }

        public virtual GalleryCategory GalleryCategory { get; set; }

        public virtual GalleryProduct GalleryProduct { get; set; }
    }
}
