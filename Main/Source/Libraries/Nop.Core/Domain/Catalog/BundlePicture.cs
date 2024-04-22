using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Media;

namespace Nop.Core.Domain.Catalog
{
    public partial class BundlePicture : BaseEntity
    {
        /// <summary>
        /// Gets or sets the bundle identifier
        /// </summary>
        public virtual int BundleId { get; set; }

        /// <summary>
        /// Gets or sets the picture identifier
        /// </summary>
        public virtual int PictureId { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public virtual int DisplayOrder { get; set; }

        /// <summary>
        /// Gets the picture
        /// </summary>
        public virtual Picture Picture { get; set; }

        /// <summary>
        /// Gets the bundle
        /// </summary>
        public virtual Bundle Bundle { get; set; }
    }
}
