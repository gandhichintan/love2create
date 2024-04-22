using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Nop.Core.Domain.Catalog;


namespace Nop.Data.Mapping.Catalog
{
   public partial class ProductHowToCategoryMap: EntityTypeConfiguration<ProductHowToCategory>
    {
        public ProductHowToCategoryMap()
        {
            this.ToTable("ProductHowTo_Category_Mapping");
            this.HasKey(pc => pc.Id);
            
            this.HasRequired(pc => pc.Category)
                .WithMany()
                .HasForeignKey(pc => pc.CategoryId);


            this.HasRequired(pc => pc.ProductHowTo)
                .WithMany(p => p.ProductHowToCategories)
                .HasForeignKey(pc => pc.ProductHowToId);
        }
    }
}
