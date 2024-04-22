using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class Faq : BaseEntity

    {
        public virtual string Question { get; set; }

        public virtual string Answer { get; set; }

        private ICollection<FaqCategoryMapping> _faqCategoryMapping;

        private ICollection<FaqCategory> _faqCategory;

        public virtual ICollection<FaqCategoryMapping> FaqCategoryMap
        {
            get { return _faqCategoryMapping ?? (_faqCategoryMapping = new List<FaqCategoryMapping>()); }
            protected set { _faqCategoryMapping = value; }
        }
        public virtual ICollection<FaqCategory> FaqCategory
        {
            get { return _faqCategory ?? (_faqCategory = new List<FaqCategory>()); }
            protected set { _faqCategory = value; }
        }
    }   
}
