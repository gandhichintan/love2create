using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class Sponsorship : BaseEntity
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Address { get; set; }

        public virtual string City { get; set; }

        public virtual int StateId { get; set; }

        public virtual string Zip { get; set; }

        public virtual string Email { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string WebSiteURL { get; set; }

        public virtual bool Event { get; set; }

        public virtual bool EditorialFeature { get; set; }

        public virtual bool Online { get; set; }

        public virtual string Description { get; set; }

        public virtual string EventName { get; set; }

        public virtual string EventWebsite { get; set; }

        public virtual DateTime EventDate { get; set; }

        public virtual bool EventSupportSite { get; set; }

        public virtual bool EventShareImage { get; set; }

        public virtual bool EventOtherProduct { get; set; }
        
        public virtual string EventDescription { get; set; }

        public virtual int EventAttendees { get; set; }

        public virtual string EditorialPublicationplaced { get; set; }

        public virtual string EditorialAppearInPrint { get; set; }

        public virtual bool EditorialOtherProduct { get; set; }

        public virtual string EditorialDescription { get; set; }

        public virtual string OnlineCommunity { get; set; }

        public virtual int OnlineAverageVisitors { get; set; }

        public virtual string OnlinePromote { get; set; }

        public virtual bool OnlineAddButton { get; set; }

        public virtual bool OnlineOtherCompanies { get; set; }

        public virtual string OnlineDescription { get; set; }

        public virtual bool Deleted { get; set; }
    }
}
