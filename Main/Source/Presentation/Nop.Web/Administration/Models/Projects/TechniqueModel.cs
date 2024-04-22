using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Projects
{
    public partial class TechniqueModel : BaseNopEntityModel
    {
        [AllowHtml]
        public string Name { get; set; }

        [AllowHtml]
        public string VideoLink { get; set; }

        [UIHint("Picture")]
        public int PictureId { get; set; }

        public bool Published { get; set; }

        public TechniqueDetailModel TechniqueDetailModel { get; set; }
    }

    public partial class TechniqueDetailModel : BaseNopEntityModel
    {
        [UIHint("TechniqueDetail")]
        public string Technique { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Categories.Fields.DisplayOrder")]
        public int DisplayOrder2 { get; set; }

        public int TechniqueId { get; set; }
        public string Title { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [UIHint("Picture")]
        public int PictureId { get; set; }

        public string VideoLink { get; set; }

        public int DisplayOrder { get; set; }
    }
}