using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Projects;

namespace Nop.Core.Domain.Catalog
{
    public partial class ProductHowToTechnique : BaseEntity
    {
        public virtual int TechniqueId { get; set; }

        public virtual int ProductHowToId { get; set; }

        public virtual int DisplayOrder { get; set; }

        public virtual ProductHowTo ProductHowTo { get; set; }

        public virtual Technique Technique { get; set; }
    }
}