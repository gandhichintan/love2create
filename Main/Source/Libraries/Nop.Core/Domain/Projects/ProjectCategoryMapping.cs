using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class ProjectCategoryMapping : BaseEntity
    {
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public virtual int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the category identifier
        /// </summary>
        public virtual int ProjectCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public virtual int DisplayOrder { get; set; }

        /// <summary>
        /// Gets the category
        /// </summary>
        public virtual ProjectCategory ProjectCategory { get; set; }

        /// <summary>
        /// Gets the product
        /// </summary>
        public virtual Project Project { get; set; }
    }
}
