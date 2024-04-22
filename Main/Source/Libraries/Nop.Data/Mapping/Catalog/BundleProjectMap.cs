using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public partial class BundleProjectMap : EntityTypeConfiguration<BundleProject>
    {
        public BundleProjectMap()
        {
            this.ToTable("Bundle_Project_Mapping");
            this.HasKey(pm => pm.Id);

            this.HasRequired(pm => pm.Bundle)
                .WithMany()
                .HasForeignKey(pm => pm.BundleId);

            this.HasRequired(pm => pm.Bundle)
                .WithMany(p => p.BundleProjects)
                .HasForeignKey(pm => pm.BundleId);
        }
    }
}
