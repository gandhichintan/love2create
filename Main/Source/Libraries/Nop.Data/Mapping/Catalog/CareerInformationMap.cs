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
    public partial class CareerInformationMap : EntityTypeConfiguration<CareerInformation>
    {
       public CareerInformationMap()
        {
            this.ToTable("CareerInformation");
            this.HasKey(p => p.Id);
            this.Property(p => p.LinkTitle);
            this.Property(p => p.Title);
            this.Property(p => p.Description);
            this.Property(p => p.YouTubeUrl);
            this.Property(p => p.CreatedOn);
        }
    }
}
