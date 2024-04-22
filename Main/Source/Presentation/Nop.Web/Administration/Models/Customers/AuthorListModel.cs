using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework;
using System.Web.Mvc;

namespace Nop.Admin.Models.Customers
{
    public partial class AuthorListModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Author Name")]
        [AllowHtml]
        public string SearchAuthorName { get; set; }

        public List<AuthorModel> Authors { get; set; }
    }
}