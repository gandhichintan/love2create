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
    public partial class SafetyMap : EntityTypeConfiguration<Safety>
    {
     public SafetyMap()
       {
           this.ToTable("Safety");
           this.Property(p => p.Id);
           this.Property(p => p.Answer);
           
        }
    }
}
