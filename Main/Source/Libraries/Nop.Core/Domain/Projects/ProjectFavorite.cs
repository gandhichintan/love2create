using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class ProjectFavorite : BaseEntity
    {
        public virtual int ProjectId { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual DateTime CreatedOn { get; set; }
    }
}
