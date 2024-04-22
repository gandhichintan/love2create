using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
 public partial class JobLink: BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string CreationDate { get; set; }
        
    }
}
