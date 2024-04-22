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
    public partial class SiteMap : EntityTypeConfiguration<Site>
    {
      public SiteMap()
        {
            this.ToTable("Site");
            this.Property(p => p.Id);
            this.Property(p => p.Name);
            this.Property(p => p.CssClass);
            this.Property(p => p.Description);
            this.Property(p => p.PictureId); 
            this.Property(p => p.URL);

        }
    }
}
