using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Projects;

namespace Nop.Data.Mapping.Projects
{
    public partial class ProjectMaterialMap : EntityTypeConfiguration<ProjectMaterial>
    {
        public ProjectMaterialMap()
        {
            this.ToTable("Project_Material_Mapping");
            this.HasKey(pc => pc.Id);
        }
    }
}
