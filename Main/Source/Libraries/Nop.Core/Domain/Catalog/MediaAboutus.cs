using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class MediaAboutus : BaseEntity
    {
        public string Title { get; set; }

        public int PictureId { get; set; }

        public string URL { get; set; }

        public Boolean IsFeatured { get; set; }

        public int FeaturedOrder { get; set; }
    }
}
