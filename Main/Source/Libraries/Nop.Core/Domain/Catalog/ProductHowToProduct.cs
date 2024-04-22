using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class ProductHowToProduct : BaseEntity
    {
        /// <summary>
        /// Gets or sets the first product identifier
        /// </summary>
        public virtual int ProductHowToId { get; set; }

        /// <summary>
        /// Gets or sets the second product identifier
        /// </summary>
        public virtual int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public virtual int DisplayOrder { get; set; }
    }
}
