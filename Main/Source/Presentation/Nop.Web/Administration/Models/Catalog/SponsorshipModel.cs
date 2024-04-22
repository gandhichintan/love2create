using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public partial class SponsorshipModel : BaseNopEntityModel
    {
        public SponsorshipModel()
        {
            this.State = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.FirstName")]
        public virtual string FirstName { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.LastName")]
        public virtual string LastName { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.Address")]
        public virtual string Address { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.City")]
        public virtual string City { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.StateId")]
        public virtual int StateId { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.State")]
        public List<SelectListItem> State { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.StateProvince")]
        public virtual string StateProvince { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.Zip")]
        public virtual string Zip { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.Email")]
        public virtual string Email { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.PhoneNumber")]
        public virtual string PhoneNumber { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.WebSiteURL")]        
        public virtual string WebSiteURL { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.Event")]
        public virtual bool Event { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.EditorialFeature")]
        public virtual bool EditorialFeature { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.Online")]
        public virtual bool Online { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.Description")]
        public virtual string Description { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.EventName")]
        public virtual string EventName { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.EventWebsite")]
        public virtual string EventWebsite { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.EventDate")]
        public virtual DateTime EventDate { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.DateOfEvent")]
        public int DateOfEvent { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.DateOfEventMonth")]
        public int DateOfEventMonth { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.DateOfEventYear")]
        public int DateOfEventYear { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.EventSupportSite")]
        public virtual bool EventSupportSite { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.EventShareImage")]
        public virtual bool EventShareImage { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.EventOtherProduct")]
        public virtual bool EventOtherProduct { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.EventDescription")]
        public virtual string EventDescription { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.EventAttendees")]
        public virtual int EventAttendees { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.EditorialPublicationplaced")]
        public virtual string EditorialPublicationplaced { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.EditorialAppearInPrint")]
        public virtual string EditorialAppearInPrint { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.EditorialOtherProduct")]
        public virtual bool EditorialOtherProduct { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.EditorialDescription")]
        public virtual string EditorialDescription { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.OnlineCommunity")]
        public virtual string OnlineCommunity { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.OnlineAverageVisitors")]
        public virtual int OnlineAverageVisitors { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.OnlinePromote")]
        public virtual string OnlinePromote { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.OnlineAddButton")]
        public virtual bool OnlineAddButton { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.OnlineOtherCompanies")]
        public virtual bool OnlineOtherCompanies { get; set; }

        [NopResourceDisplayName("Admin.Sponsorship.Fields.OnlineDescription")]
        public virtual string OnlineDescription { get; set; }

        public string Result { get; set; }

        public bool IsSuccessful { get; set; }    

    }
}