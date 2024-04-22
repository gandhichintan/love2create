using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class Event : BaseEntity 
    {
        public virtual DateTime CreatedOnUtc { get; set; }

        public virtual string Name { get; set; }

        public virtual string Url { get; set; }

        public virtual string Description { get; set; }

        public virtual string Host { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual string StartTime { get; set; }

        public virtual DateTime EndDate { get; set; }

        public virtual string EndTime { get; set; }

        public virtual string Location { get; set; }

        public virtual string Email { get; set; }

        public virtual string Address { get; set; }

        public virtual string Address2 { get; set; }

        public virtual int Category { get; set; }

        public virtual string Zip { get; set; }

        public virtual string City { get; set; }

        public virtual int StateId { get; set; }

        public virtual int CountryId { get; set; }

        public virtual int SiteId { get; set; }

        public virtual string State1 { get; set; }

        public virtual string State2 { get; set; }

        public virtual string State3 { get; set; }

        public virtual int LogoImageId { get; set; }

        public virtual int EevntImageId { get; set; }

        public virtual string NonUSState { get; set; }

        public virtual bool Approved { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual bool Published { get; set; }

        public virtual bool ShowOnCommunity { get; set; }
    }
}
