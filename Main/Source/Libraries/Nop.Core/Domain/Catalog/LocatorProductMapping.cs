using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class LocatorProductMapping : BaseEntity
    {
       
        /// <summary>
        /// Gets or sets the LocatorId
        /// </summary>
        public int LocatorId { get; set; }

        /// <summary>
        /// Gets or sets the ProductId
        /// </summary>
        public int ProductId { get; set; }

        public virtual Locator Locator { get; set; }

        /// <summary>
        /// Gets the product
        /// </summary>
        public virtual Product Product { get; set; }
   

    }
}
