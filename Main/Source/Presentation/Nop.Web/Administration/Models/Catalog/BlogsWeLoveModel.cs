using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public partial class BlogsWeLoveModel : BaseNopEntityModel
    {
        [AllowHtml]
        public string Title { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [UIHint("Picture")]
        public int PictureId { get; set; }

        [AllowHtml]
        public string Url { get; set; }

        public bool Published { get; set; }
    }
}