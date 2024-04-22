using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public partial class BundlePictureMap : EntityTypeConfiguration<BundlePicture>
    {
        public BundlePictureMap()
        {
            this.ToTable("Bundle_Picture_Mapping");
            this.HasKey(pp => pp.Id);

            /*this.HasRequired(pp => pp.Picture)
                .WithMany(p => p.BundlePictures)
                .HasForeignKey(pp => pp.PictureId);*/

            this.HasRequired(pp => pp.Bundle)
                .WithMany(p => p.BundlePictures)
                .HasForeignKey(pp => pp.BundleId);
        }
    }
}
