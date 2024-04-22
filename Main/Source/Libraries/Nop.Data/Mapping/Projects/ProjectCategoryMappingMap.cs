using System.Data.Entity.ModelConfiguration;
using Nop.Core.Domain.Projects;

namespace Nop.Data.Mapping.Projects
{
    public partial class ProjectCategoryMappingMap : EntityTypeConfiguration<ProjectCategoryMapping>
    {
        public ProjectCategoryMappingMap()
        {
            this.ToTable("ProjectCategoryMapping");
            this.HasKey(c => c.Id);
        }
    }
}
