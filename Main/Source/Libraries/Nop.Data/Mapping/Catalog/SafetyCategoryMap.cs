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
    public partial class SafetyCategoryMap : EntityTypeConfiguration<SafetyCategory>
    {
        public SafetyCategoryMap()
        {
            this.ToTable("SafetyCategory");
            this.Property(p => p.Id);
            this.Property(p => p.Name);

        }
    }
}

