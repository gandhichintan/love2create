﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class SiteWeLove : BaseEntity
    {
        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual string SiteUrl { get; set; }

        public virtual bool Published { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual DateTime CreatedOnUtc { get; set; }

        public virtual DateTime UpdatedOnUtc { get; set; }
    }
}
