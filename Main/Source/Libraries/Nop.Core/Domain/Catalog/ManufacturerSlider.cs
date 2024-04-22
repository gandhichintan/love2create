using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class ManufacturerSlider : BaseEntity
    {
        public int ManufacturerId { get; set; }
        public int PictureId { get; set; }
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
    }
}
