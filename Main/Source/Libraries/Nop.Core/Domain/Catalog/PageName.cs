using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class PageName : BaseEntity
    {
        public string ServerPath { get; set; }

        public string pagename { get; set; }
    }
}
