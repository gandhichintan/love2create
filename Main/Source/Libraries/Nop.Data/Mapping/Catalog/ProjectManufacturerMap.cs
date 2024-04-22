using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public partial class ProjectManufacturerMap : EntityTypeConfiguration<ProjectManufacturer>
    {
        public ProjectManufacturerMap()
        {
            this.ToTable("Project_Manufacturer_Mapping");
            this.HasKey(pm => pm.Id);
            
            this.HasRequired(pm => pm.Manufacturer)
                .WithMany()
                .HasForeignKey(pm => pm.ManufacturerId);

            this.HasRequired(pm => pm.Project)
                .WithMany(p => p.ProjectManufacturers)
                .HasForeignKey(pm => pm.ProjectId);
        }
    }
}
