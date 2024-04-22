using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    class ProductHowToMap: EntityTypeConfiguration<ProductHowTo>
    {
        public ProductHowToMap()
        {
            this.ToTable("ProductHowTo");
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(400);
            this.Property(p => p.ShortDescription);
            this.Property(p => p.FullDescription);
        }
    
    }
}
