using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class CareerOpening : BaseEntity
    {
        public string Title { get; set; }

        public string Email { get; set; }

        public string SendResumeTo { get; set;}

        public string ConfidentialFax { get; set; }

        public string InPerson { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
