using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Projects;

namespace Nop.Data.Mapping.Projects
{
    public partial class DistributorLinkMap : EntityTypeConfiguration<DistributorLink>
    {
        public DistributorLinkMap()
        {
            this.ToTable("DistributorLink");
            this.HasKey(c => c.Id);
        }
    }
}
