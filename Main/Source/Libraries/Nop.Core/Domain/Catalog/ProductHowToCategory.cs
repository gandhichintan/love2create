using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class ProductHowToCategory : BaseEntity
    {
        public virtual int ProductHowToId { get; set; }

        /// <summary>
        /// Gets or sets the category identifier
        /// </summary>
        public virtual int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product is featured
        /// </summary>
        public virtual bool IsFeaturedProductHowTo { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public virtual int DisplayOrder { get; set; }

        /// <summary>
        /// Gets the category
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Gets the product
        /// </summary>
        public virtual ProductHowTo ProductHowTo { get; set; }

    }
}
