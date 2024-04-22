using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class SurveyCategoryRegistration : BaseEntity
    {
        public virtual int RegistrationId { get; set; }

        public virtual int CategoryId { get; set; }
    }
}
