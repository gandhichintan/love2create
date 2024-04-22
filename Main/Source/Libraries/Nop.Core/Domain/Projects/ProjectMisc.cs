using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class ProjectMisc : BaseEntity
    {
        public virtual int ProjectId { get; set; }
        public virtual string Description { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual bool Published { get; set; }
        public virtual DateTime CreatedOn { get; set; }
    }
}
