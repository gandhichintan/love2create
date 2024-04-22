using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Seo;

namespace Nop.Core.Domain.Catalog
{
    public partial class ProductHowTo : BaseEntity, ISlugSupported
    {
        // private ICollection<ProductVariant> _productVariants;
        private ICollection<ProductHowToCategory> _producthowtoCategories;
        private ICollection<ProductHowToManufacturer> _producthowtoManufacturers;
        private ICollection<ProductHowToPicture> _producthowtoPictures;
        private ICollection<ProductHowToVideo> _producthowtoVideo;
        private ICollection<ProductHowToBundle> _producthowtoBundle;
        private ICollection<ProductHowToTechnique> _producthowtoTechnique;

        // private ICollection<ProductReview> _productReviews;
        // private ICollection<ProductSpecificationAttribute> _productSpecificationAttributes;
        //  private ICollection<ProductTag> _productTags;
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
        /// Gets or sets the admin comment
        /// </summary>

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public virtual bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public virtual bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time of product creation
        /// </summary>
        public virtual DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of product update
        /// </summary>
        public virtual DateTime UpdatedOnUtc { get; set; }

        public virtual int Views { get; set; }

        public virtual string HowToUse { get; set; }

        public virtual string TipsAndTricks { get; set; }

        /// <summary>
        /// Gets or sets the product variants
        /// </summary>
        /// <summary>
        /// Gets or sets the collection of ProductCategory
        /// </summary>
        public virtual ICollection<ProductHowToCategory> ProductHowToCategories
        {
            get { return _producthowtoCategories ?? (_producthowtoCategories = new List<ProductHowToCategory>()); }
            protected set { _producthowtoCategories = value; }
        }

        /// <summary>
        /// Gets or sets the collection of ProductManufacturer
        /// </summary>
        public virtual ICollection<ProductHowToManufacturer> ProductHowToManufacturers
        {
            get { return _producthowtoManufacturers ?? (_producthowtoManufacturers = new List<ProductHowToManufacturer>()); }
            protected set { _producthowtoManufacturers = value; }
        }

        /// <summary>
        /// Gets or sets the collection of ProductPicture
        /// </summary>
        public virtual ICollection<ProductHowToPicture> ProductHowToPictures
        {
            get { return _producthowtoPictures ?? (_producthowtoPictures = new List<ProductHowToPicture>()); }
            protected set { _producthowtoPictures = value; }
        }
        public virtual ICollection<ProductHowToBundle> ProductHowToBundle
        {
            get { return _producthowtoBundle ?? (_producthowtoBundle = new List<ProductHowToBundle>()); }
            protected set { _producthowtoBundle = value; }
        }
        public virtual ICollection<ProductHowToVideo> ProductHowToVideo
        {
            get { return _producthowtoVideo ?? (_producthowtoVideo = new List<ProductHowToVideo>()); }
            protected set { _producthowtoVideo = value; }
        }
        public virtual ICollection<ProductHowToTechnique> ProductHowToTechnique
        {
            get { return _producthowtoTechnique ?? (_producthowtoTechnique = new List<ProductHowToTechnique>()); }
            protected set { _producthowtoTechnique = value; }
        }

        /// <summary>
        /// Gets or sets the collection of product reviews
        /// </summary>

        /// <summary>
        /// Gets or sets the product specification attribute
        /// </summary>

        /// <summary>
        /// Gets or sets the product specification attribute
        /// </summary>

    }
}
