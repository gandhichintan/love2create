using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Customers
{
    public partial class DistributorProfile : BaseEntity
    {
        public Guid DistributorProfileGUID { get; set; }
        public int CustomerID { get; set; }
        public int RoleID { get; set; }
        public bool isEnabled { get; set; }
        public bool isNew { get; set; }
        public bool isApproved { get; set; }
        public DateTime signUpDate { get; set; }
        public string URL { get; set; }
        public string faxNumber { get; set; }
    }
}
