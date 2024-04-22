using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Projects;

namespace Nop.Data.Mapping.Projects
{
    public partial class ArticleCommentMappingMap : EntityTypeConfiguration<ArticleCommentMapping>
    {
        public ArticleCommentMappingMap()
        {
            this.ToTable("ArticleCommentMapping");
            this.HasKey(c => c.Id);

            this.HasRequired(pc => pc.Customer)
                .WithMany()
                .HasForeignKey(pc => pc.CustomerId);

            this.HasRequired(pc => pc.Project)
                .WithMany()
                .HasForeignKey(pc => pc.ProjectId);

            this.HasRequired(pc => pc.ArticleComment)
                .WithMany(p => p.ArticleCommentMap)
                .HasForeignKey(pc => pc.CommentId);
        }
    }
}
