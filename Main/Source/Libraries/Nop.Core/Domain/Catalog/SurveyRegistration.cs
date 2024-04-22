using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class SurveyRegistration : BaseEntity
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Email { get; set; }

        public virtual DateTime DOB { get; set; }

        public virtual string Zip { get; set; }

        public virtual string City { get; set; }

        public virtual int TypeId { get; set; }

        public virtual bool GlobalOpinionPanel { get; set; }

        public virtual bool FocusGroupPanel { get; set; }

        public virtual bool Active { get; set; }

        public virtual string Representation { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual bool Deleted { get; set; }
    }
}
