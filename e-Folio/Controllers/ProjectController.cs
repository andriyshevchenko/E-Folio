using System.Collections.Generic;

using e_folio.data;
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
        private IRepository<Project> projects;

        public ProjectController(IRepository<Project> repository)
        {
            projects = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetProjects()
        {
            return Ok(projects.GetItemsList());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<Project>> GetProject(int id)
        {
            Project project = projects.GetItem(id);
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
            projects.Delete(id);
            return Ok();
        }

        [HttpPost]
        public ActionResult MakeNewProject([FromBody] Project project)
        {
            projects.Add(project);
            return Ok();
        }
         
        [HttpPut]
        public ActionResult Edit([FromBody] Project project)
        {
            projects.Update(project);
            return Ok();
        }
    }
}