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
    public partial class TrendMap : EntityTypeConfiguration<Trend>
    {
     public TrendMap()
     {
         this.ToTable("Trend");
         this.Property(p => p.Id);
         this.Property(p=>p.EndDate);
         this.Property(p => p.Name);
         this.Property(p => p.PictureId);
         this.Property(p => p.StartDate);
         
     }
    }
}
