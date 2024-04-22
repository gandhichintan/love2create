using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework;
using System.Web.Mvc;

namespace Nop.Admin.Models.Projects
{
    public partial class VideoListModel : BaseNopModel
    {
        [NopResourceDisplayName("Video Name")]
        [AllowHtml]
        public string SearchVideoName { get; set; }

        public List<VideoModel> Videos { get; set; }
    }
}