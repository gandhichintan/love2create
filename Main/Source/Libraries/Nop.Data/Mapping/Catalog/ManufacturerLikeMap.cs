using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public partial class ManufacturerLikeMap : EntityTypeConfiguration<ManufacturerLike>
    {
        public ManufacturerLikeMap()
        {
            this.ToTable("ManufacturerLike");
            this.HasKey(c => c.Id);
        }
    }
}
