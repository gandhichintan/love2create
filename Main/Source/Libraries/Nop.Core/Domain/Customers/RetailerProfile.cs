using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Customers
{
    public partial class RetailerProfile : BaseEntity
    {
        public virtual Guid RetailerProfileGUID { get; set; }
        public virtual int CustomerID { get; set; }
        public virtual int RoleID { get; set; }
        public virtual bool isEnabled { get; set; }
        public virtual bool isNew { get; set; }
        public virtual bool isApproved { get; set; }
        public virtual DateTime signUpDate { get; set; }
        public virtual string storeName { get; set; }
        public virtual string distributorName { get; set; }
        public virtual string URL { get; set; }
        public virtual string faxNumber { get; set; }
    }
}
