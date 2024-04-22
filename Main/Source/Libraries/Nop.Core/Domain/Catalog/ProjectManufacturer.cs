using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Projects;

namespace Nop.Core.Domain.Catalog
{
    public partial class ProjectManufacturer : BaseEntity
    {
        public virtual int ProjectId { get; set; }

        public virtual int ManufacturerId { get; set; }

        public virtual int DisplayOrder { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual Project Project { get; set; }
    }
}
