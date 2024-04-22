


using Nop.Core.Domain.Catalog;
using System.Data.Entity.ModelConfiguration;
namespace Nop.Data.Mapping.Catalog
{
    public partial class TrendReportMap : EntityTypeConfiguration<TrendReport>
    {
        public TrendReportMap()
        {
            this.ToTable("TrendReport");
            this.Property(p => p.Id);
            this.Property(p => p.CreationDate);
            this.Property(p => p.Name);
            this.Property(p => p.Order);
            this.Property(p => p.PdfPath);
            this.Property(p => p.PictureId);
        }
    }
}
