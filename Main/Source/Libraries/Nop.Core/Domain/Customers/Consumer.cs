using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Customers
{
    public partial class Consumer : BaseEntity
    {
        public virtual Guid ConsumerGUID { get; set; }
        public virtual string Name { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual int ParentConsumerID { get; set; }
        public virtual string SEName { get; set; }
        public virtual string ExtensionData { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual int SkinID { get; set; }
        public virtual string TemplateName { get; set; }
    }
}
