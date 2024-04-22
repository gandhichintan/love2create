using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Catalog
{
    public partial class ProductFaqMap : EntityTypeConfiguration<ProductFaq>
    {
        public ProductFaqMap()
        {
            this.ToTable("ProductFaq");
        }
    }
}
