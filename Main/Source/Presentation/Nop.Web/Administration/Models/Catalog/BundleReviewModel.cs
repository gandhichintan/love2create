using System;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Admin.Validators.Catalog;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public class BundleReviewModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Manufacture")]
        public int BundleId { get; set; }
        [NopResourceDisplayName("Manufacture")]
        public string BundleName { get; set; }

        [NopResourceDisplayName("Customer")]
        public int CustomerId { get; set; }

        [NopResourceDisplayName("IPAddress")]
        public string IpAddress { get; set; }

        [AllowHtml]
        [NopResourceDisplayName("Title")]
        public string Title { get; set; }

        [AllowHtml]
        [NopResourceDisplayName("ReviewText")]
        public string ReviewText { get; set; }

        [NopResourceDisplayName("Rating")]
        public int Rating { get; set; }

        [NopResourceDisplayName("IsApproved")]
        public bool IsApproved { get; set; }

        [NopResourceDisplayName("CreatedOn")]
        public DateTime CreatedOn { get; set; }
    }
}