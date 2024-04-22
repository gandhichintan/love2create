using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
 public partial   class ProductHowToTechniqueMap : EntityTypeConfiguration<ProductHowToTechnique>
    {

     public ProductHowToTechniqueMap()
            {
                this.ToTable("ProductHowTo_Technique_Mapping");
                this.HasKey(pm => pm.Id);

                this.HasRequired(pm => pm.Technique)
                    .WithMany()
                    .HasForeignKey(pm => pm.TechniqueId);

                this.HasRequired(pm => pm.ProductHowTo)
                    .WithMany(p => p.ProductHowToTechnique)
                    .HasForeignKey(pm => pm.ProductHowToId);
            }
   }
}
