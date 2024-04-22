using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Admin.Models.Catalog;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Projects
{
    public partial class UserGalleryModel :BaseNopEntityModel
    {
        [AllowHtml]
        public string Name { get; set; }

        [AllowHtml]
        public string Email { get; set; }

        [UIHint("Date")]
        public DateTime Birthday { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [AllowHtml]
        public string ProductsUsed { get; set; }

        [UIHint("Picture")]
        public int PictureId { get; set; }

        public bool CraftsNewsletter { get; set; }

        public bool CeramicNewsletter { get; set; }

        public bool EducationNewsletter { get; set; }

        public bool IsApproved { get; set; }

        public bool IsFeatured { get; set; }

        public bool Published { get; set; }
    }
}