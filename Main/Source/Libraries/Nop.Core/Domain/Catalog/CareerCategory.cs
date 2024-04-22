using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class CareerCategory : BaseEntity
    {
        public string Title { get; set; }

        public string IdCareerCategory { get; set; }
    }
}
