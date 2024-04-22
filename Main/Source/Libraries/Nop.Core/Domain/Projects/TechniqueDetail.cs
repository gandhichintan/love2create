using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class TechniqueDetail : BaseEntity
    {
        public virtual int TechniqueId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual int PictureId { get; set; }
        public virtual string VideoLink { get; set; }
        public virtual int DisplayOrder { get; set; }
    }
}
