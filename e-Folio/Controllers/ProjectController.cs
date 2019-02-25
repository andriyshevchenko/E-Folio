using System.Collections.Generic;

using eFolio.DTO;
using eFolio.BL;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc;

namespace eFolio
{
    [Route("api/projects")]
    [Produces("application/json")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetProjects()
        {
            return Ok(projectService.GetItemsList());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<Project>> GetProject(int id)
        {
            Project project = projectService.GetItem(id);
            if (project == null)
            {
                return NotFound(id);
            }
            return Ok(project);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteProject(int id)
        {
            projectService.Delete(id);
            return Ok();
        }

        [HttpPost]
        public ActionResult MakeNewProject([FromBody] Project project)
        {
            projectService.Add(project);
            return Ok();
        }
         
        [HttpPut]
        public ActionResult Edit([FromBody] Project project)
        {
            projectService.Update(project);
            return Ok();
        }
    }
}