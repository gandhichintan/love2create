using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class ArticleComment : BaseEntity
    {
        public virtual string Comment { get; set; }

        public virtual int Rating { get; set; }

        public virtual DateTime PublishedDate { get; set; }

        public virtual bool IsApproved { get; set; }

        private ICollection<ArticleCommentMapping> _articleCommentMapping;

        public virtual ICollection<ArticleCommentMapping> ArticleCommentMap
        {
            get { return _articleCommentMapping ?? (_articleCommentMapping = new List<ArticleCommentMapping>()); }
            protected set { _articleCommentMapping = value; }
        }
    }
}
