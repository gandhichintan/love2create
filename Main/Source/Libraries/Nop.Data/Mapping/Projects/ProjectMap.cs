using System.Data.Entity.ModelConfiguration;
using Nop.Core.Domain.Projects;

namespace Nop.Data.Mapping.Projects
{
    public partial class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
        {
            this.ToTable("Project");
            this.HasKey(c => c.Id);
        }
    }
}
