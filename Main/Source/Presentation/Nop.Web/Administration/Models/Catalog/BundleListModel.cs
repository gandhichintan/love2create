using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public partial class BundleListModel : BaseNopModel
    {
        [AllowHtml]
        public string SearchBundleName { get; set; }

        public List<BundleModel> Bundles { get; set; }
    }
}