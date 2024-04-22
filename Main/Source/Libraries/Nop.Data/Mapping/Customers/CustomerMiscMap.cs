using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Customers;

namespace Nop.Data.Mapping.Customers
{
    public partial class CustomerMiscMap : EntityTypeConfiguration<CustomerMisc>
    {
        public CustomerMiscMap()
        {
            this.ToTable("CustomerMisc");
            this.HasKey(c => c.Id);
        }
    }
}
