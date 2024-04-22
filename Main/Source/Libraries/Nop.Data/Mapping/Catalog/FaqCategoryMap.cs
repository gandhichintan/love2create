using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;


namespace Nop.Data.Mapping.Catalog
{
    public partial class FaqCategoryMap : EntityTypeConfiguration<FaqCategory>
    {
        public FaqCategoryMap()
        {
            this.ToTable("FaqCategory");
            this.HasKey(p => p.Id);
        }
    }
}
