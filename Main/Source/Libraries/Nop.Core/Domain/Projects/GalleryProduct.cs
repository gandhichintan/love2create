using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class GalleryProduct : BaseEntity
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual int PictureId { get; set; }

        public virtual bool Published { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual DateTime CreatedOnUtc { get; set; }

        public virtual DateTime UpdatedOnUtc { get; set; }

        private ICollection<GalleryProductCategory> _galleryProductCategories;

        public virtual ICollection<GalleryProductCategory> GalleryProductCategories
        {
            get { return _galleryProductCategories ?? (_galleryProductCategories = new List<GalleryProductCategory>()); }
            protected set { _galleryProductCategories = value; }
        }
    }
}
