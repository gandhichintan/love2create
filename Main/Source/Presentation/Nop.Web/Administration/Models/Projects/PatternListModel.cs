using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework;
using System.Web.Mvc;

namespace Nop.Admin.Models.Projects
{
    public partial class PatternListModel : BaseNopModel
    {
        
        [NopResourceDisplayName("Pattern Name")]
        [AllowHtml]
        public string SearchPatternName { get; set; }

        public List<PatternModel> Patterns { get; set; }
    }
}