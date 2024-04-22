using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class Site : BaseEntity
    {
        public string Name {get;set;}

        public string URL {get;set;}
    
        public string CssClass {get;set;}

        public string Description {get;set;}

        public int PictureId {get;set;}

    }
}
