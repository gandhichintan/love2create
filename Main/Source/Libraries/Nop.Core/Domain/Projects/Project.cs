using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Seo;

namespace Nop.Core.Domain.Projects
{
    public partial class Project : BaseEntity, ISlugSupported
    {
        private ICollection<ProjectReview> _projectReviews;
        private ICollection<ProjectTag> _projectTags;
        private ICollection<ProjectManufacturer> _projectManufacturers;
        private ICollection<ProjectCat> _projectCat;
        private ICollection<ProductHowToProject> _producthowtoProject;

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Caption { get; set; }
        public virtual string Notes { get; set; }
        public virtual string Keywords { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual DateTime? ProjectOfTheDay { get; set; }
        public virtual string AudioFilePath { get; set; }
        public virtual int Views { get; set; }
        public virtual bool Published { get; set; }
        public virtual bool IsArchived { get; set; }
        public virtual bool IsArticle { get; set; }
        public virtual bool IsTechnique { get; set; }
        public virtual bool IsRoundup { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual bool Featured { get; set; }
        public virtual bool ShowOnHomePage { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual DateTime UpdatedOn { get; set; }
        public virtual DateTime? PublishedDate { get; set; }
        public virtual bool ShowOnCommunity { get; set; }

        /// <summary>
        /// Gets or sets the rating sum (approved reviews)
        /// </summary>
        public virtual int ApprovedRatingSum { get; set; }

        /// <summary>
        /// Gets or sets the rating sum (not approved reviews)
        /// </summary>
        public virtual int NotApprovedRatingSum { get; set; }

        /// <summary>
        /// Gets or sets the total rating votes (approved reviews)
        /// </summary>
        public virtual int ApprovedTotalReviews { get; set; }

        /// <summary>
        /// Gets or sets the total rating votes (not approved reviews)
        /// </summary>
        public virtual int NotApprovedTotalReviews { get; set; }

        /// <summary>
        /// Gets or sets the collection of project reviews
        /// </summary>
        public virtual ICollection<ProjectReview> ProjectReviews
        {
            get { return _projectReviews ?? (_projectReviews = new List<ProjectReview>()); }
            protected set { _projectReviews = value; }
        }

        /// <summary>
        /// Gets or sets the product specification attribute
        /// </summary>
        public virtual ICollection<ProjectTag> ProjectTags
        {
            get { return _projectTags ?? (_projectTags = new List<ProjectTag>()); }
            protected set { _projectTags = value; }
        }

        public virtual ICollection<ProjectManufacturer> ProjectManufacturers
        {
            get { return _projectManufacturers ?? (_projectManufacturers = new List<ProjectManufacturer>()); }
            protected set { _projectManufacturers = value; }
        }

        public virtual ICollection<ProjectCat> ProjectCat
        {
            get { return _projectCat ?? (_projectCat = new List<ProjectCat>()); }
            protected set { _projectCat = value; }
        }

        public virtual ICollection<ProductHowToProject> ProductHowToProject
        {
            get { return _producthowtoProject ?? (_producthowtoProject = new List<ProductHowToProject>()); }
            protected set { _producthowtoProject = value; }
        }
    }
}
