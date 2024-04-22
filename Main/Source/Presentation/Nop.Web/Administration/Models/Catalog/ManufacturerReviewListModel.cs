using System;
using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public class ManufacturerReviewListModel
    {
        [NopResourceDisplayName("CreatedFrom")]
        [UIHint("DateNullable")]
        public DateTime? CreatedOnFrom { get; set; }

        [NopResourceDisplayName("CreatedTo")]
        [UIHint("DateNullable")]
        public DateTime? CreatedOnTo { get; set; }
    }
}