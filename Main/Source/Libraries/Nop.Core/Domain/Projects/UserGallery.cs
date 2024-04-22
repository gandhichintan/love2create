using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Projects
{
    public partial class UserGallery : BaseEntity
    {
        public virtual int CustomerId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual DateTime Birthday { get; set; }

        public virtual string Description { get; set; }

        public virtual int PictureId { get; set; }

        public virtual string ProductsUsed { get; set; }

        public virtual bool CraftsNewsletter { get; set; }

        public virtual bool CeramicNewsletter { get; set; }

        public virtual bool EducationNewsletter { get; set; }

        public virtual bool IsApproved { get; set; }

        public virtual bool IsFeatured { get; set; }

        public virtual bool Published { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual DateTime UpdatedOn { get; set; }
    }
}
