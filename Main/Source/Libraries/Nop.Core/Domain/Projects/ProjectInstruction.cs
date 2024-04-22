using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class ProjectInstruction : BaseEntity
    {
        public virtual int ProjectId { get; set; }
        public virtual string Title { get; set; }
        public virtual string InstructionDescription { get; set; }
        public virtual int PictureId { get; set; }
        public virtual string InstructionVideo { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual bool Published { get; set; }
        public virtual DateTime CreatedOn { get; set; }
    }
}
