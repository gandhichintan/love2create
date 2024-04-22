using System;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Admin.Validators.Catalog;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Catalog
{    
    public class ManufacturerReviewModel: BaseNopEntityModel
    {
        [NopResourceDisplayName("Brand")]
        public int ManufacturerId { get; set; }
        [NopResourceDisplayName("Brand")]
        public string ManufacturerName { get; set; }

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
