using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class Pattern : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual int PictureId { get; set; }
        public virtual bool Published { get; set; }
        public virtual DateTime CreatedOn { get; set; }
    }
}
