using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public partial class ManufacturerReviewMap : EntityTypeConfiguration<ManufacturerReview>
    {
        public ManufacturerReviewMap()
        {
            this.ToTable("ManufacturerReview");

            this.Property(pr => pr.Title);
            this.Property(pr => pr.ReviewText);

            this.HasRequired(pr => pr.Manufacturer)
                .WithMany(p => p.ManufacturerReviews)
                .HasForeignKey(pr => pr.ManufacturerId);
        }
    }
}
