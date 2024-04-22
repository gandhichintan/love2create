using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework;
using System.Web.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public partial class SiteWeLoveListModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Site Name")]
        [AllowHtml]
        public string SearchSiteName { get; set; }

        public List<SiteWeLoveModel> SiteWeLoves { get; set; }
    }
}