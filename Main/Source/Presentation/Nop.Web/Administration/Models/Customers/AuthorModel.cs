using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;


namespace Nop.Admin.Models.Customers
{
    public partial class AuthorModel : BaseNopEntityModel
    {
        [AllowHtml]
        public string Title { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [UIHint("Picture")]
        public int PictureId { get; set; }
        
        public bool Published { get; set; }
    }
}