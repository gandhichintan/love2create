using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class AmbassadorLinksProduct : BaseEntity
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual int PictureId { get; set; }

        public virtual bool Published { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual DateTime CreatedOnUtc { get; set; }

        public virtual DateTime UpdatedOnUtc { get; set; }

        private ICollection<AmbassadorLinksProductCategory> _ambassadorLinksProductCategories;

        public virtual ICollection<AmbassadorLinksProductCategory> AmbassadorLinksProductCategories
        {
            get { return _ambassadorLinksProductCategories ?? (_ambassadorLinksProductCategories = new List<AmbassadorLinksProductCategory>()); }
            protected set { _ambassadorLinksProductCategories = value; }
        }
    }
}
