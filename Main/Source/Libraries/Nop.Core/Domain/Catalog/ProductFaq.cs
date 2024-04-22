using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a product
    /// </summary>
    public partial class ProductFaq : BaseEntity
    {
        
        /// <summary>
        /// Gets or sets the ProductId
        /// </summary>
        public virtual int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the Faq Question
        /// </summary>
        public virtual string FaqQuestion { get; set; }

        /// <summary>
        /// Gets or sets the Faq Answer
        /// </summary>
        public virtual string FaqAnswer { get; set; }

        /// <summary>
        /// Gets or sets the Display Order
        /// </summary>
        public virtual int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the Products
        /// </summary>
        public virtual Product Product { get; set; }
    }
}