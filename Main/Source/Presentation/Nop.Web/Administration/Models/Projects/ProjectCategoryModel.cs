using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Admin.Validators.Catalog;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Projects
{
    public partial class ProjectCategoryModel : BaseNopEntityModel
    {
        public ProjectCategoryModel()
        {

        }

        [AllowHtml]
        public string Name { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        public int ParentCategoryId { get; set; }

        public int DisplayOrder { get; set; }

        [UIHint("Picture")]
        public int PictureId { get; set; }

        public bool ShowOnHomePage { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public int ProjectId { get; set; }

        public bool ShowInMenu { get; set; }

        public bool ShowInSidebar { get; set; }

        public bool DoNotShow { get; set; }

        public IList<SelectListItem> ParentProjectCategories { get; set; }
    }
}