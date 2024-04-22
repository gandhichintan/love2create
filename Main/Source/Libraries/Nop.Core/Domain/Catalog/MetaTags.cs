using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class MetaTags : BaseEntity 
    {
        public string TitleTag { get; set; }

        public string DescriptionTag { get; set; }

        public string KeywordsTag { get; set; }

        public string PageTitle { get; set; }

        public string URL { get; set; }

        public DateTime CreationDate { get; set; }
        
 
    }
}
