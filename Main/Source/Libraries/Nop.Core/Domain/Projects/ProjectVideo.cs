using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class ProjectVideo : BaseEntity
    {
        public virtual int VideoId { get; set; }
        public virtual int ProjectId { get; set; }
        public virtual int DisplayOrder { get; set; }

        public virtual Video Video { get; set; }

        public virtual Project Project { get; set; }
    }
}
