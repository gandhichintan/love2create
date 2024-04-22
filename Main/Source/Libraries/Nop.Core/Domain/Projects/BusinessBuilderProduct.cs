using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class BusinessBuilderProduct : BaseEntity
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual int PictureId { get; set; }

        public virtual bool Published { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual DateTime CreatedOnUtc { get; set; }

        public virtual DateTime UpdatedOnUtc { get; set; }

        private ICollection<BusinessBuilderProductCategory> _businessBuilderProductCategories;

        public virtual ICollection<BusinessBuilderProductCategory> BusinessBuilderProductCategories
        {
            get { return _businessBuilderProductCategories ?? (_businessBuilderProductCategories = new List<BusinessBuilderProductCategory>()); }
            protected set { _businessBuilderProductCategories = value; }
        }
    }
}
