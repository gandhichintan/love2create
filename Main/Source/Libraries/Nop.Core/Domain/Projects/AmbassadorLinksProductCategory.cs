using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class AmbassadorLinksProductCategory : BaseEntity
    {
        public virtual int AmbassadorLinksProductId { get; set; }

        public virtual int AmbassadorLinksCategoryId { get; set; }

        public virtual int DisplayOrder { get; set; }

        public virtual AmbassadorLinksCategory AmbassadorLinksCategory { get; set; }

        public virtual AmbassadorLinksProduct AmbassadorLinksProduct { get; set; }
    }
}
