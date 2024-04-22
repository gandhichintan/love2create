using System.Data.Entity.ModelConfiguration;
using Nop.Core.Domain.Projects;

namespace Nop.Data.Mapping.Projects
{
    public partial class AmbassadorProfileMap : EntityTypeConfiguration<AmbassadorProfile>
    {
        public AmbassadorProfileMap()
        {
            this.ToTable("AmbassadorProfile");
            this.HasKey(c => c.Id);
        }
    }
}
