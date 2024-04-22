using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
   public partial  class Locator : BaseEntity
    {
       private ICollection<LocatorProductMapping> _locatorProduct;

       public  string Name { get; set; }

        /// <summary>
       /// Gets or sets the URL
        /// </summary>
       public string URL { get; set; }

        /// <summary>
       /// Gets or sets the CountryId
        /// </summary>
       public int CountryId { get; set; }

       /// <summary>
       /// Gets or sets a value of IsInternational
       /// </summary>
       public bool IsInternational { get; set; }

        /// <summary>
       /// Gets or sets the IdLocator
        /// </summary>
       public string IdLocator { get; set; }


       public virtual ICollection<LocatorProductMapping> LocatorProduct
       {
           get { return _locatorProduct ?? (_locatorProduct = new List<LocatorProductMapping>()); }
           protected set { _locatorProduct = value; }
       }
       


    }
}
