using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework;
using System.Web.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public partial class EventListModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Event Name")]
        [AllowHtml]
        public string SearchEventName { get; set; }

        public List<EventModel> Events { get; set; }
    }
}