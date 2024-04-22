using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class FaqCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Headline { get; set; }
             
    }
}
