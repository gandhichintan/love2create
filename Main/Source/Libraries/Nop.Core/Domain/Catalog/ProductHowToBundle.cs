using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
   public partial class ProductHowToBundle : BaseEntity
    {
        public virtual int BundleId { get; set; }

        public virtual int ProductHowToId { get; set; }

        public virtual int DisplayOrder { get; set; }

        public virtual Bundle Bundle { get; set; }

        public virtual ProductHowTo ProductHowTo { get; set; }
    }
}
