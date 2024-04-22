using System.Data.Entity.ModelConfiguration;
using Nop.Core.Domain.Projects;

namespace Nop.Data.Mapping.Projects
{
    public partial class ProjectReviewMap : EntityTypeConfiguration<ProjectReview>
    {
        public ProjectReviewMap()
        {
            this.ToTable("ProjectReview");
            //this.HasKey(c => c.Id);
        }
    }
}
