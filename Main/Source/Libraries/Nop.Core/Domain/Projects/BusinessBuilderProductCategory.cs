using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class BusinessBuilderProductCategory : BaseEntity
    {
        public virtual int BusinessBuilderProductId { get; set; }

        public virtual int BusinessBuilderCategoryId { get; set; }

        public virtual int DisplayOrder { get; set; }

        public virtual BusinessBuilderCategory BusinessBuilderCategory { get; set; }

        public virtual BusinessBuilderProduct BusinessBuilderProduct { get; set; }
    }
}
