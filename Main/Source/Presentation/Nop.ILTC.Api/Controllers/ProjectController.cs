using Nop.Core;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nop.Services.Projects;

namespace Nop.ILTC.Api.Controllers
{
    [RoutePrefix("api/project")]
    public class ProjectController : ApiController
    {

        #region"Private Variable(s)"
        private readonly IProjectService _projectService;
        #endregion

        #region"Constructor"
        public ProjectController(IProjectService projectService)
        {
            this._projectService = projectService;
        }

        #endregion

        #region"Public Action(s)"

        [HttpGet]
        [Route("getall")]
        public IHttpActionResult GetAllProject()
        {
            try
            {
                var projects = _projectService.GetAllProjects();
                return Ok("Project api fired");
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        #region projects

        [HttpDelete]
        public IHttpActionResult DeleteProject(Project project)
        {
            try
            {
                _projectService.DeleteProject(project);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("getallfeaturedprojectsdisplayedonhomepage")]
        public IHttpActionResult GetAllFeaturedProjectsDisplayedOnHomePage()
        {
            try
            {
                var featuredProject = _projectService.GetAllFeaturedProjectsDisplayedOnHomePage();
                return Ok(featuredProject);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("getallfeaturedprojectdisplayedoncommunitypage")]
        public IHttpActionResult GetAllFeaturedProjectsDisplayedOnCommunityPage()
        {
            try
            {
                var featuredProjectsForCOmmunityPage = _projectService.GetAllFeaturedProjectsDisplayedOnCommunityPage();
                return Ok(featuredProjectsForCOmmunityPage);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("getprojectbyid")]
        public IHttpActionResult GetProjectById(int projectId)
        {
            try
            {
                var project = _projectService.GetProjectById(projectId);
                return Ok(project);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("getprojectsbyids")]
        public IHttpActionResult GetProjectsByIds(int[] projectIds)
        {
            try
            {
                var projects = _projectService.GetProjectsByIds(projectIds);
                return Ok(projects);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult InsertProject(Project project)
        {
            try
            {
                if (ModelState.IsValid)
                    _projectService.InsertProject(project);
                else
                    return BadRequest(ModelState.Values.Select(x => x.Errors).ToString());
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateProject(Project project)
        {
            try
            {
                if (ModelState.IsValid)
                    _projectService.UpdateProject(project);
                else
                    return BadRequest(ModelState.Values.Select(x => x.Errors).ToString());
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("getallprojectsdisplayonhomepage")]
        public IHttpActionResult GetAllProjectsDisplayedOnHomePage()
        {
            try
            {
                var projects = _projectService.GetAllProjectsDisplayedOnHomePage();
                return Ok(projects);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        #endregion

        #endregion
    }
}
