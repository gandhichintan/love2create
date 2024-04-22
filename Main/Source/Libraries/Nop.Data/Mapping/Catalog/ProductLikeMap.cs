using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public partial class ProductLikeMap : EntityTypeConfiguration<ProductLike>
    {
        public ProductLikeMap()
        {
            this.ToTable("ProductLike");
            this.HasKey(c => c.Id);
        }
    }
}
