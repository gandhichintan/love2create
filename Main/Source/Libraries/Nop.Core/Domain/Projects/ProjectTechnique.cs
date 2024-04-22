using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class ProjectTechnique : BaseEntity
    {
        public virtual int ProjectId { get; set; }
        public virtual int TechniqueId { get; set; }
        public virtual int DisplayOrder { get; set; }
    }
}
