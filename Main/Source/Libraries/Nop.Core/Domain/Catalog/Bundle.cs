using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;

namespace Nop.Core.Domain.Catalog
{
    public partial class Bundle : BaseEntity, ILocalizedEntity, ISlugSupported
    {
        private ICollection<BundlePicture> _bundlePictures;
        private ICollection<BundleProduct> _bundleProducts;
        private ICollection<BundleProject> _bundleProjects;
        private ICollection<BundleReview> _bundleReviews;
        private ICollection<ProductHowToBundle> _producthowtoBundle;

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the short description
        /// </summary>
        public virtual string ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the full description
        /// </summary>
        public virtual string FullDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the bundle on home page
        /// </summary>
        public virtual bool ShowOnHomePage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public virtual bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public virtual bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time of bundle creation
        /// </summary>
        public virtual DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of bundle update
        /// </summary>
        public virtual DateTime UpdatedOnUtc { get; set; }

        public virtual int Views { get; set; }

        public virtual int ApprovedRatingSum { get; set; }

        public virtual int NotApprovedRatingSum { get; set; }

        public virtual int ApprovedTotalReviews { get; set; }

        public virtual int NotApprovedTotalReviews { get; set; }

        /// <summary>
        /// Gets or sets the collection of BundlePicture
        /// </summary>
        public virtual ICollection<BundlePicture> BundlePictures
        {
            get { return _bundlePictures ?? (_bundlePictures = new List<BundlePicture>()); }
            protected set { _bundlePictures = value; }
        }

        public virtual ICollection<BundleProduct> BundleProducts
        {
            get { return _bundleProducts ?? (_bundleProducts = new List<BundleProduct>()); }
            protected set { _bundleProducts = value; }
        }

        public virtual ICollection<BundleProject> BundleProjects
        {
            get { return _bundleProjects ?? (_bundleProjects = new List<BundleProject>()); }
            protected set { _bundleProjects = value; }
        }

        public virtual ICollection<BundleReview> BundleReviews
        {
            get { return _bundleReviews ?? (_bundleReviews = new List<BundleReview>()); }
            protected set { _bundleReviews = value; }
        }

        public virtual ICollection<ProductHowToBundle> ProductHowToBundle
        {
            get { return _producthowtoBundle ?? (_producthowtoBundle = new List<ProductHowToBundle>()); }
            protected set { _producthowtoBundle = value; }
        }
    }
}
