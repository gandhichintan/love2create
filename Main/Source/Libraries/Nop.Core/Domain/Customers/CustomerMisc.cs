using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Customers
{
    public partial class CustomerMisc : BaseEntity
    {
        public virtual Guid CustomerMiscGUID { get; set; }
        public virtual int CustomerID { get; set; }
        public virtual bool CraftNewsletter { get; set; }
        public virtual bool CeramicNewsletter { get; set; }
        public virtual string Suggestion { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual int ConsumerID { get; set; }
        public virtual bool EducationNewsletter { get; set; }
        public virtual string Email { get; set; }
        public virtual bool Active { get; set; }
    }
}
