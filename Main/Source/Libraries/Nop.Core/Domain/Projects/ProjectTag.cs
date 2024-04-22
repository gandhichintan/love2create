using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Localization;

namespace Nop.Core.Domain.Projects
{
    public partial class ProjectTag : BaseEntity, ILocalizedEntity
    {
        private ICollection<Project> _projects;

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the tagged product count
        /// </summary>
        public virtual int ProjectCount { get; set; }

        /// <summary>
        /// Gets or sets the product variants
        /// </summary>
        public virtual ICollection<Project> Projects
        {
            get { return _projects ?? (_projects = new List<Project>()); }
            protected set { _projects = value; }
        }
    }
}
