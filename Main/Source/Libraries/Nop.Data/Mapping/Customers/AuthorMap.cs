using System.Data.Entity.ModelConfiguration;
using Nop.Core.Domain.Customers;

namespace Nop.Data.Mapping.Customers
{
    public partial class  AuthorMap : EntityTypeConfiguration<Author>
    {
        public AuthorMap()
        {
            this.ToTable("Author");
            this.HasKey(c => c.Id);
        }
    }
}
    