using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework;
using System.Web.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public partial class BookListModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Book Name")]
        [AllowHtml]
        public string SearchBookName { get; set; }

        public List<BookModel> Books { get; set; }
    }
}