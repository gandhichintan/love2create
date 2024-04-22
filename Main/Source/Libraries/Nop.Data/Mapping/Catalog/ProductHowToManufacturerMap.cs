using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
  public partial  class ProductHowToManufacturerMap: EntityTypeConfiguration<ProductHowToManufacturer>
    {
      public ProductHowToManufacturerMap()
        {
            this.ToTable("ProductHowTo_Manufacturer_Mapping");
            this.HasKey(pm => pm.Id);
            
            this.HasRequired(pm => pm.Manufacturer)
                .WithMany()
                .HasForeignKey(pm => pm.ManufacturerId);


            this.HasRequired(pm => pm.ProductHowTo)
                .WithMany(p => p.ProductHowToManufacturers)
                .HasForeignKey(pm => pm.ProductHowToId);
        }
    }
}
