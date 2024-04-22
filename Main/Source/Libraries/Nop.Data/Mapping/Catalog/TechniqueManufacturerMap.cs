using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public partial class TechniqueManufacturerMap : EntityTypeConfiguration<TechniqueManufacturer>
    {
        public TechniqueManufacturerMap()
        {
            this.ToTable("Technique_Manufacturer_Mapping");
            this.HasKey(pm => pm.Id);
            
            this.HasRequired(pm => pm.Manufacturer)
                .WithMany()
                .HasForeignKey(pm => pm.ManufacturerId);

            this.HasRequired(pm => pm.Technique)
                .WithMany(p => p.TechniqueManufacturers)
                .HasForeignKey(pm => pm.TechniqueId);
        }
    }
}
