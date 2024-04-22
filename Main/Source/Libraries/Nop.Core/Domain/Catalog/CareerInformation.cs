using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class CareerInformation : BaseEntity
    {
        public string LinkTitle { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string YouTubeUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
