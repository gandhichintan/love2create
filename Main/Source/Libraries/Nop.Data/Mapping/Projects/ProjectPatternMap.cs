using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Projects;

namespace Nop.Data.Mapping.Projects
{
    public partial class ProjectPatternMap : EntityTypeConfiguration<ProjectPattern>
    {
        public ProjectPatternMap()
        {
            this.ToTable("ProjectPattern");
            this.HasKey(c => c.Id);
        }
    }
}
