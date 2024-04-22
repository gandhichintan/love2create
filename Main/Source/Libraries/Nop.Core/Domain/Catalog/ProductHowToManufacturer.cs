using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class ProductHowToManufacturer : BaseEntity
    {
        public virtual int ProductHowToId { get; set; }

        /// <summary>
        /// Gets or sets the manufacturer identifier
        /// </summary>
        public virtual int ManufacturerId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product is featured
        /// </summary>
        public virtual bool IsFeaturedProductHowTo { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public virtual int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the manufacturer
        /// </summary>
        public virtual Manufacturer Manufacturer { get; set; }

        /// <summary>
        /// Gets or sets the product
        /// </summary>
        public virtual ProductHowTo ProductHowTo { get; set; }

    }
}
