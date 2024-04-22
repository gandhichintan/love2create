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
    public partial class FaqCategoryMappingMap : EntityTypeConfiguration<FaqCategoryMapping>
    {
        public FaqCategoryMappingMap()
        {
            this.ToTable("Faq_Category_Mapping");
            this.HasKey(p => p.Id);

            this.HasRequired(pc => pc.FaqCategory)
                .WithMany()
                .HasForeignKey(pc => pc.FaqCategoryId);

            this.HasRequired(pc => pc.Faq)
                .WithMany(p => p.FaqCategoryMap)
                .HasForeignKey(pc => pc.FaqId);
        }
    }
}
