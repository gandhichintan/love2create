using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Projects;

namespace Nop.Data.Mapping.Catalog
{
    public partial class UserGalleryMap : EntityTypeConfiguration<UserGallery>
    {
        public UserGalleryMap()
        {
            this.ToTable("UserGallery");
            this.HasKey(c => c.Id);
        }
    }
}
