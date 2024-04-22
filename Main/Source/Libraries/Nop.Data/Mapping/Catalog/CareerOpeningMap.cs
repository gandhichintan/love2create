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
    public partial class CareerOpeningMap : EntityTypeConfiguration<CareerOpening>
    {
        public CareerOpeningMap()
        {
            this.ToTable("CareerOpening");
            this.HasKey(p => p.Id);
            this.Property(p => p.Title);
            this.Property(p => p.SendResumeTo);
            this.Property(p => p.InPerson);
            this.Property(p => p.Email);
            this.Property(p => p.ConfidentialFax);
            this.Property(p => p.CreationDate);
        }
    }
}
