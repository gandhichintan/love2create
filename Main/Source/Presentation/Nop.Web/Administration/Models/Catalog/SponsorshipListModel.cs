using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework;
using System.Web.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public partial class SponsorshipListModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Sponsor Name")]
        [AllowHtml]
        public string SearchSponsorName { get; set; }

        public List<SponsorshipModel> Sponsorships { get; set; }
    }
}