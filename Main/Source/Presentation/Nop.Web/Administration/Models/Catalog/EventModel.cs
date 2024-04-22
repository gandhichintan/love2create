using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public partial class EventModel : BaseNopEntityModel
    {
        public EventModel()
        {
            //this.Category = new List<SelectListItem>();

            this.State = new List<SelectListItem>();

            this.Country = new List<SelectListItem>();

            //Events = new List<EventModel>();
        }
        [NopResourceDisplayName("Admin.Event.Fields.CreatedOnUtc")]
        public virtual DateTime CreatedOnUtc { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.Name")]
        public virtual string Name { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.Url")]
        public virtual string Url { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.Description")]
        public virtual string Description { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.Host")]
        public virtual string Host { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.StartDate")]
        public virtual DateTime StartDate { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.StartDateDay")]
        public virtual int StartDateDay { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.StartDateMonth")]
        public virtual int StartDateMonth { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.StartDateYear")]
        public virtual int StartDateYear { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.StartTime")]
        public virtual string StartTime { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.EndDate")]
        public  DateTime EndDate { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.EndDay")]
        public int EndDay { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.EndMonth")]
        public int EndMonth { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.EndYear")]
        public int EndYear { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.EndTime")]
        public virtual string EndTime { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.Location")]
        public virtual string Location { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.Email")]
        public virtual string Email { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.Address")]
        public virtual string Address { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.Address2")]
        public virtual string Address2 { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.CategoryId")]
        public int CategoryId { get; set; }

        //[NopResourceDisplayName("Admin.Event.Fields.Category")]
        //public IList<SelectListItem> Category { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.Category")]
        public virtual string Categories { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.Zip")]
        public virtual string Zip { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.City")]
        public virtual string City { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.StateId")]
        public virtual int StateId { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.State")]
        public IList<SelectListItem> State { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.Country")]
        public IList<SelectListItem> Country { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.CountryId")]
        public virtual int CountryId { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.SiteId")]
        public virtual int SiteId { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.State1")]
        public virtual string State1 { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.State2")]
        public virtual string State2 { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.State3")]
        public virtual string State3 { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.LogoImage")]
        [UIHint("Picture")]
        public virtual int LogoImageId { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.EevntImage")]
        [UIHint("Picture")]
        public virtual int EevntImageId { get; set; }

        //[NopResourceDisplayName("Admin.Event.Fields.LogoImagePath")]
        //public string LogoImagePath { get; set; }

        //[NopResourceDisplayName("Admin.Event.Fields.EventImagePath")]
        //public string EventImagePath { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.NonUSState")]
        public virtual string NonUSState { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.Approved")]
        public virtual bool Approved { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.Deleted")]
        public virtual bool Deleted { get; set; }

        [NopResourceDisplayName("Admin.Event.Fields.Published")]
        public virtual bool Published { get; set; }

        public virtual bool ShowOnCommunity { get; set; }

        public string Result { get; set; }

        public bool IsSuccessful { get; set; }
    }
}