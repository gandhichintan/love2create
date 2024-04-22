using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;
using System.Data.Entity.ModelConfiguration;
namespace Nop.Data.Mapping.Catalog
{
    public partial  class EventMap : EntityTypeConfiguration<Event>
    {
        public EventMap()
        {
            this.ToTable("Event");
            this.HasKey(c => c.Id);
        }
    }
}
