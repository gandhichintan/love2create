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
    public partial class PatternModel : BaseNopEntityModel
    {
        [AllowHtml]
        public string Name { get; set; }

        [UIHint("Picture")]
        public int PictureId { get; set; }

        [AllowHtml]
        public string PictureUrl { get; set; }

        public bool Published { get; set; }
    }

    public partial class ProjectPatternModel : BaseNopEntityModel
    {
        [UIHint("ProjectPattern")]
        public string Pattern { get; set; }

        public int ProjectId { get; set; }

        public int PatternId { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Categories.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }
    }
}