
using System.Collections.Generic;
using System.Web.WebPages.Html;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework;
using System.Web.Mvc;
namespace Nop.Admin.Models.Projects
{
    public partial class ProjectListModel : BaseNopModel
    {
        public ProjectListModel()
        {

        }

        [NopResourceDisplayName("Project Name")]
        [AllowHtml]
        public string SearchProjectName { get; set; }

        public List<ProjectModel> Projects { get; set; }
    }
}