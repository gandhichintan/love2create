using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Customers
{
    public partial class Author : BaseEntity
    {
        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual int PictureId { get; set; }

        public virtual bool Published { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual DateTime UpdatedOn { get; set; }

    }
}
