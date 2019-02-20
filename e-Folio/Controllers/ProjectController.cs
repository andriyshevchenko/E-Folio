using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_folio.core.Entities;
using e_folio.data;
using eFolio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_Folio
{
    [Route("api/projects")]
    [Produces("application/json")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IRepository<ProjectEntity> projects;

        public ProjectController(IRepository<ProjectEntity> repository)
        {
            projects = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProjectEntity>> GetProjects()
        {
            return Ok(projects.GetItemsList());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<ProjectEntity>> GetProject(int id)
        {
            ProjectEntity project = projects.GetItem(id);
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
        public ActionResult MakeNewProject([FromBody] ProjectEntity project)
        {
            projects.Add(project);
            return Ok();
        }
         

    }
}