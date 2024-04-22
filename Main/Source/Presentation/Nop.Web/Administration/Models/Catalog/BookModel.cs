using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Catalog 
{
    public partial class BookModel : BaseNopEntityModel
    {
        public BookModel()
        {
            this.Authors = new List<SelectListItem>();
        }

        [AllowHtml]
        public string Title { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [AllowHtml]
        public string BookUrl { get; set; }

        [UIHint("Picture")]
        public int PictureId { get; set; }
      
        public bool Published { get; set; }

        public string AuthorId { get; set; }

        public IList<SelectListItem> Authors { get; set; }
    }
}