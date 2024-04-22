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
    public partial class VideoModel : BaseNopEntityModel
    {
        [AllowHtml]
        public string Name { get; set; }

        [AllowHtml]
        public string Url { get; set; }

        [AllowHtml]
        public string EmbedScript { get; set; }

        public bool Published { get; set; }
    }

    public partial class ProjectVideoModel : BaseNopEntityModel
    {
        [UIHint("ProjectVideo")]
        public string Video { get; set; }

        public int ProjectId { get; set; }

        public int VideoId { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Categories.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }
    }
}