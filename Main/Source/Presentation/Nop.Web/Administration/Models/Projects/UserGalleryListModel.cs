using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework;
using System.Web.Mvc;

namespace Nop.Admin.Models.Projects
{
    public class UserGalleryListModel : BaseNopModel
    {
        [NopResourceDisplayName("Gallery Name")]
        [AllowHtml]
        public string SearchGalleryName { get; set; }

        public List<UserGalleryModel> UserGalleries { get; set; }
    }
}