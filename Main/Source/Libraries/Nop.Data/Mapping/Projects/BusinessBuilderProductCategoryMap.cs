using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Projects;

namespace Nop.Data.Mapping.Projects
{
    public partial class BusinessBuilderProductCategoryMap : EntityTypeConfiguration<BusinessBuilderProductCategory>
    {
        public BusinessBuilderProductCategoryMap()
        {
            this.ToTable("BusinessBuilderProduct_Category_Mapping");
            this.HasKey(pc => pc.Id);

            this.HasRequired(pc => pc.BusinessBuilderCategory)
                .WithMany()
                .HasForeignKey(pc => pc.BusinessBuilderCategoryId);


            this.HasRequired(pc => pc.BusinessBuilderProduct)
                .WithMany(p => p.BusinessBuilderProductCategories)
                .HasForeignKey(pc => pc.BusinessBuilderProductId);
        }
    }
}
