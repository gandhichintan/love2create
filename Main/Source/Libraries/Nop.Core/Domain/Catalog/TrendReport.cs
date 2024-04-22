using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class TrendReport : BaseEntity
    {
        public int Order { get; set; }

        public int PictureId { get; set; }

        public string Name { get; set; }

        public string PdfPath { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
