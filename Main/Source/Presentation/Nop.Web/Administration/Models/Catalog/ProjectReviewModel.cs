using System;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Admin.Validators.Catalog;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Catalog
{
    [Validator(typeof(ProjectReviewValidator))]
    public partial class ProjectReviewModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Admin.Catalog.ProductReviews.Fields.Product")]
        public int ProjectId { get; set; }
        [NopResourceDisplayName("Project")]
        public string ProjectName { get; set; }

        [NopResourceDisplayName("Admin.Catalog.ProductReviews.Fields.Customer")]
        public int CustomerId { get; set; }

        [NopResourceDisplayName("Admin.Catalog.ProductReviews.Fields.IPAddress")]
        public string IpAddress { get; set; }

        [AllowHtml]
        [NopResourceDisplayName("Admin.Catalog.ProductReviews.Fields.Title")]
        public string Title { get; set; }

        [AllowHtml]
        [NopResourceDisplayName("Admin.Catalog.ProductReviews.Fields.ReviewText")]
        public string ReviewText { get; set; }

        [NopResourceDisplayName("Admin.Catalog.ProductReviews.Fields.Rating")]
        public int Rating { get; set; }

        [NopResourceDisplayName("Admin.Catalog.ProductReviews.Fields.IsApproved")]
        public bool IsApproved { get; set; }

        [NopResourceDisplayName("Admin.Catalog.ProductReviews.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }
    }
}