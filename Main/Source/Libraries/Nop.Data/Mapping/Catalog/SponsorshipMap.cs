using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;


namespace Nop.Data.Mapping.Catalog
{
    public partial class SponsorshipMap : EntityTypeConfiguration<Sponsorship>
    {
        public SponsorshipMap()
        {
            this.ToTable("Sponsorship");
            this.HasKey(c => c.Id);
        }
    }
}
