using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Customers;

namespace Nop.Core.Domain.Catalog
{
    public partial class BundleReview : CustomerContent
    {
        public virtual int BundleId { get; set; }

        public virtual string Title { get; set; }

        public virtual string ReviewText { get; set; }

        public virtual int Rating { get; set; }

        public virtual Bundle Bundle { get; set; }
    }
}
