using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Projects;

namespace Nop.Core.Domain.Catalog
{
     public partial class ProjectCat : BaseEntity
    {
        public virtual int ProjectId { get; set; }

        public virtual int CategoryId { get; set; }

        public virtual int DisplayOrder { get; set; }

        public virtual Category Category { get; set; }

        public virtual Project Project { get; set; }
    }
}
