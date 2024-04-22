using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Projects;

namespace Nop.Core.Domain.Catalog
{
    public partial class CategoryVideo : BaseEntity
    {
        public virtual int VideoId { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual int DisplayOrder { get; set; }

        public virtual Video Video { get; set; }

        public virtual Category Category { get; set; }
    }
}
