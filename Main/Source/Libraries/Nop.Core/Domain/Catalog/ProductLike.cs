﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class ProductLike : BaseEntity
    {
        public virtual int ProductId { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual DateTime CreatedOn { get; set; }
    }
}
