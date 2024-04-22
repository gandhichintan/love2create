using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class Follower : BaseEntity
    {
        public virtual int ArtistId { get; set; }
        public virtual int FollowerId { get; set; }
        public virtual DateTime CreatedOn { get; set; }
    }
}
