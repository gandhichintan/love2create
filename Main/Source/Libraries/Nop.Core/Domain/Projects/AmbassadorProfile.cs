using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class AmbassadorProfile : BaseEntity
    {
        public virtual Guid AmbassadorProfileGUID { get; set; }

        public virtual int CustomerID { get; set; }

        public virtual int RoleID { get; set; }

        public virtual bool isEnabled { get; set; }

        public virtual bool isNew { get; set; }

        public virtual bool isApproved { get; set; }

        public virtual DateTime signUpDate { get; set; }

        public virtual string imagePath { get; set; }

        public virtual int years { get; set; }

        public virtual string areasServiced { get; set; }

        public virtual string certification { get; set; }

        public virtual string specialities { get; set; }

        public virtual string comments { get; set; }

        public virtual string URL { get; set; }

        public virtual string faxNumber { get; set; }

    }
}
