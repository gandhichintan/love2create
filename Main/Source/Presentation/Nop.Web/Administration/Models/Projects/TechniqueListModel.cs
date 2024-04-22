using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework;
using System.Web.Mvc;

namespace Nop.Admin.Models.Projects
{
    public class TechniqueListModel : BaseNopModel
    {
        [NopResourceDisplayName("Technique Name")]
        [AllowHtml]
        public string SearchTechniqueName { get; set; }

        public List<TechniqueModel> Techniques { get; set; }
    }
}