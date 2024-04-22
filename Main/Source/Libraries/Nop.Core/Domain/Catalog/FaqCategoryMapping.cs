using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class FaqCategoryMapping : BaseEntity
    {
        /// </summary>
        public int FaqId { get; set; }

        public int FaqCategoryId { get; set; }

        public virtual FaqCategory FaqCategory { get; set; }

        public virtual Faq Faq { get; set; }

    }
}
