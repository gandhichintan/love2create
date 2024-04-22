using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public partial class BundleReviewMap : EntityTypeConfiguration<BundleReview>
    {
        public BundleReviewMap()
        {
            this.ToTable("BundleReview");

            this.Property(pr => pr.Title);
            this.Property(pr => pr.ReviewText);

            this.HasRequired(pr => pr.Bundle)
                .WithMany(p => p.BundleReviews)
                .HasForeignKey(pr => pr.BundleId);
        }
    }
}
