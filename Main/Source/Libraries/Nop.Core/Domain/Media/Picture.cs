using System.Collections.Generic;
using Nop.Core.Domain.Catalog;

namespace Nop.Core.Domain.Media
{
    /// <summary>
    /// Represents a picture
    /// </summary>
    public partial class Picture : BaseEntity
    {
        private ICollection<ProductPicture> _productPictures;
        private ICollection<BundlePicture> _bundlePictures;
        private ICollection<ProductHowToPicture> _producthowtoPictures;
        /// <summary>
        /// Gets or sets the picture binary
        /// </summary>
        public byte[] PictureBinary { get; set; }

        /// <summary>
        /// Gets or sets the picture mime type
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the SEO friednly filename of the picture
        /// </summary>
        public string SeoFilename { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the picture is new
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Gets or sets the product pictures
        /// </summary>
        public virtual ICollection<ProductPicture> ProductPictures
        {
            get { return _productPictures ?? (_productPictures = new List<ProductPicture>()); }
            protected set { _productPictures = value; }
        }

        public virtual ICollection<BundlePicture> BundlePictures
        {
            get { return _bundlePictures ?? (_bundlePictures = new List<BundlePicture>()); }
            protected set { _bundlePictures = value; }
        }

        public virtual ICollection<ProductHowToPicture> ProductHowToPictures
        {
            get { return _producthowtoPictures ?? (_producthowtoPictures = new List<ProductHowToPicture>()); }
            protected set { _producthowtoPictures = value; }
        }
    }
}
