using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;
using System.Data.Entity.ModelConfiguration;
namespace Nop.Data.Mapping.Catalog
{
 public partial   class ProductHowToBundleMap : EntityTypeConfiguration<ProductHowToBundle>
    {
     public ProductHowToBundleMap()
     {

         this.ToTable("ProductHowTo_Bundle_Mapping");
         this.HasKey(pm => pm.Id);

         this.HasRequired(pm => pm.Bundle)
             .WithMany()
             .HasForeignKey(pm => pm.BundleId);

         this.HasRequired(pm => pm.Bundle)
             .WithMany(p => p.ProductHowToBundle)
             .HasForeignKey(pm => pm.BundleId);
     }
     }
}
