using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Projects;

namespace Nop.Data.Mapping.Projects
{
    public partial class AmbassadorLinksProductCategoryMap : EntityTypeConfiguration<AmbassadorLinksProductCategory>
    {
        public AmbassadorLinksProductCategoryMap()
        {
            this.ToTable("AmbassadorLinksProduct_Category_Mapping");
            this.HasKey(pc => pc.Id);

            this.HasRequired(pc => pc.AmbassadorLinksCategory)
                .WithMany()
                .HasForeignKey(pc => pc.AmbassadorLinksCategoryId);


            this.HasRequired(pc => pc.AmbassadorLinksProduct)
                .WithMany(p => p.AmbassadorLinksProductCategories)
                .HasForeignKey(pc => pc.AmbassadorLinksProductId);
        }
    }
}
