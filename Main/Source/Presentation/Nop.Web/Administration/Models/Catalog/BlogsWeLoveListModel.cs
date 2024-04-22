using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework;
using System.Web.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public partial class BlogsWeLoveListModel : BaseNopModel
    {
        [NopResourceDisplayName("Blog Name")]
        [AllowHtml]
        public string SearchBlogName { get; set; }

        public List<BlogsWeLoveModel> BlogsWeLoves { get; set; }
    }
}