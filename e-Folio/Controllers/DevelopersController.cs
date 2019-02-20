using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_folio.core.Entities;
using e_folio.data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_Folio
{
    [Route("api/developers")]
    [Produces("application/json")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private IRepository<DeveloperEntity> developers;
        private IRepository<ProjectEntity> projects;

        public DevelopersController(IRepository<DeveloperEntity> developers,
                                    IRepository<ProjectEntity> projects)
        {
            this.developers = developers;
            this.projects = projects;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DeveloperEntity>> GetDevelopers()
        {
            return Ok(developers.GetItemsList());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<DeveloperEntity>> GetDeveloper(int id)
        {
            DeveloperEntity project = developers.GetItem(id);
            if (project == null)
            {
                return NotFound(id);
            }
            return Ok(project);
        }

        [HttpPost]
        public ActionResult NewDeveloper([FromBody] DeveloperEntity developer)
        {
            developers.Add(developer);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteDeveloper(int id)
        {
            developers.Delete(id);
            return Ok();
        }

        [HttpPut]
        public ActionResult Edit(DeveloperEntity developer)
        {
            developers.Update(developer);
            return Ok();
        }

        [HttpPut]
        [Route("/api/projects/{projectId}/devs/{id}")]
        public ActionResult AssignToProject(int projectId, int id)
        {
            ProjectEntity project = projects.GetItem(projectId);
            if (project == null)
            {
                return NotFound("Project not found: " + projectId);
            }

            DeveloperEntity developer = developers.GetItem(id);
            if (developer == null)
            {
                return NotFound("Developer does not exist: " + id);
            }

            if (project.Developers == null)
            {
                project.Developers = new List<ProjectDeveloperEntity>();
            }
            project.Developers.Add(new ProjectDeveloperEntity()
            { 
                DeveloperId = developer.Id,
                ProjectId = project.Id
            });

            projects.Update(project);

            return Ok();
        }
    }
}