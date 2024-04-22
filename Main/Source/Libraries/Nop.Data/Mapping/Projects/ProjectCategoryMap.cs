using Nop.Core.Domain.Projects;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Data.Mapping.Projects
{
    public partial class ProjectCategoryMap : EntityTypeConfiguration<ProjectCategory>
    {
        public ProjectCategoryMap()
        {
            this.ToTable("ProjectCategory");
            this.HasKey(c => c.Id);
        }
    }
}
