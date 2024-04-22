using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Customers;

namespace Nop.Core.Domain.Projects
{
    public partial class ProjectCustomer : BaseEntity
    {
        public virtual int CustomerId { get; set; }
        public virtual int ProjectId { get; set; }
        public virtual int DisplayOrder { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Project Project { get; set; }
    }
}
