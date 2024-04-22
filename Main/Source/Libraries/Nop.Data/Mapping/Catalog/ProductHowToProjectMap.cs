using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
  public partial  class ProductHowToProjectMap: EntityTypeConfiguration<ProductHowToProject>
    {
        public ProductHowToProjectMap()
        {
            this.ToTable("ProductHowTo_Project_Mapping");
            this.HasKey(pm => pm.Id);
            
            this.HasRequired(pm => pm.ProductHowTo)
                .WithMany()
                .HasForeignKey(pm => pm.ProductHowToId);

            this.HasRequired(pm => pm.Project)
                .WithMany(p => p.ProductHowToProject)
                .HasForeignKey(pm => pm.ProjectId);
        }
    }
}