using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class Subtopic : BaseEntity
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

    }
}
