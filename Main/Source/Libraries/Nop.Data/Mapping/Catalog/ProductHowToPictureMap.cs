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
   public partial class ProductHowToPictureMap: EntityTypeConfiguration<ProductHowToPicture>
    {
        public ProductHowToPictureMap()
        {
            this.ToTable("ProductHowTo_Picture_Mapping");
            this.HasKey(pp => pp.Id);

            /*this.HasRequired(pp => pp.Picture)
                .WithMany(p => p.ProductHowToPictures)
                .HasForeignKey(pp => pp.PictureId);*/


            this.HasRequired(pp => pp.ProductHowTo)
                .WithMany(p => p.ProductHowToPictures)
                .HasForeignKey(pp => pp.ProductHowToId);
        } 
    }
}
