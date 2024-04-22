using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Core.Domain.Projects
{
    public partial class ProjectMaterial : BaseEntity
    {
        public virtual int ProjectId { get; set; }

        public virtual int CategoryId { get; set; }

        public virtual int ProductId { get; set; }

        public virtual int DisplayOrder { get; set; }

        public virtual bool IsFeatured { get; set; }

        public virtual Project Project { get; set; }

        public virtual Category Category { get; set; }

        public virtual Product Product { get; set; }
    }
}
