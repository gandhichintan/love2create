using System.Collections.Generic;
using Nop.Core.Domain.Customers;

namespace Nop.Core.Domain.Projects
{
    /// <summary>
    /// Represents a product review
    /// </summary>
    public partial class ProjectReview : CustomerContent
    {
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public virtual int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the review text
        /// </summary>
        public virtual string ReviewText { get; set; }

        /// <summary>
        /// Review rating
        /// </summary>
        public virtual int Rating { get; set; }

        /// <summary>
        /// Gets the product
        /// </summary>
        public virtual Project Project { get; set; }
    }
}
