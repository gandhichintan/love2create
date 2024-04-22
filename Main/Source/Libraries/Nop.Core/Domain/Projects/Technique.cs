using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Core.Domain.Projects
{
    public partial class Technique : BaseEntity
    {
        private ICollection<TechniqueManufacturer> _techniqueManufacturers;

        public virtual string Name { get; set; }
        public virtual string VideoLink { get; set; }
        public virtual int PictureId { get; set; }
        public virtual bool Published { get; set; }
        public virtual DateTime CreatedOn { get; set; }

        public virtual ICollection<TechniqueManufacturer> TechniqueManufacturers
        {
            get { return _techniqueManufacturers ?? (_techniqueManufacturers = new List<TechniqueManufacturer>()); }
            protected set { _techniqueManufacturers = value; }
        }
    }
}
