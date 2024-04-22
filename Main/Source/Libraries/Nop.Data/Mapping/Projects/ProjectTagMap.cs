using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Projects;

namespace Nop.Data.Mapping.Projects
{
    public partial class ProjectTagMap : EntityTypeConfiguration<ProjectTag>
    {
        public ProjectTagMap()
        {
            this.ToTable("ProjectTag");
            this.HasKey(pt => pt.Id);
            this.Property(pt => pt.Name).IsRequired().HasMaxLength(400);

            this.HasMany(pt => pt.Projects)
                .WithMany(p => p.ProjectTags)
                .Map(m => m.ToTable("Project_ProjectTag_Mapping"));
        }
    }
}
