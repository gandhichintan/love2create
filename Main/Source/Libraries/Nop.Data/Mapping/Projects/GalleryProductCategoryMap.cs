using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Projects;

namespace Nop.Data.Mapping.Projects
{
    public partial class GalleryProductCategoryMap : EntityTypeConfiguration<GalleryProductCategory>
    {
        public GalleryProductCategoryMap()
        {
            this.ToTable("GalleryProduct_Category_Mapping");
            this.HasKey(pc => pc.Id);

            this.HasRequired(pc => pc.GalleryCategory)
                .WithMany()
                .HasForeignKey(pc => pc.GalleryCategoryId);


            this.HasRequired(pc => pc.GalleryProduct)
                .WithMany(p => p.GalleryProductCategories)
                .HasForeignKey(pc => pc.GalleryProductId);
        }
    }
}
