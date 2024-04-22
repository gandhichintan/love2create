using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public partial class ProjectCatMap  : EntityTypeConfiguration<ProjectCat>
    {
         public ProjectCatMap()
        {
            this.ToTable("Project_Category_Mapping");
            this.HasKey(pm => pm.Id);
            
            this.HasRequired(pm => pm.Category)
                .WithMany()
                .HasForeignKey(pm => pm.CategoryId);

            this.HasRequired(pm => pm.Project)
                .WithMany(p => p.ProjectCat)
                .HasForeignKey(pm => pm.ProjectId);
        }
    } 
}
