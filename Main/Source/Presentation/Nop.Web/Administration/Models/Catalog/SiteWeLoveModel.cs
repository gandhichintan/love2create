using System.Web.Mvc;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public partial class SiteWeLoveModel : BaseNopEntityModel
    {
        [AllowHtml]
        public string Title { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [AllowHtml]
        public string SiteUrl { get; set; }

        public bool Published { get; set; }
    }
}