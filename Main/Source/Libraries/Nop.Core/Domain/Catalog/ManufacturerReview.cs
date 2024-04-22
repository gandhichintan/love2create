using Nop.Core.Domain.Customers;

namespace Nop.Core.Domain.Catalog
{
    public partial class ManufacturerReview : CustomerContent
    {
        public virtual int ManufacturerId { get; set; }

        public virtual string Title { get; set; }

        public virtual string ReviewText { get; set; }

        public virtual int Rating { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
