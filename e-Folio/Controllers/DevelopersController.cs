using System.Collections.Generic;
using System.Linq;

using e_folio.data;
using eFolio.BL;
using eFolio.EF;
using Microsoft.AspNetCore.Mvc;

namespace eFolio
{
    [Route("api/developers")]
    [Produces("application/json")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private IRepository<Developer> developers;
        private IRepository<Project> projects;

        public DevelopersController(IRepository<Developer> developers,
                                    IRepository<Project> projects)
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
            var project = developers.GetItem(id);
            if (project == null)
            {
                return NotFound(id);
            }
            return Ok(project);
        }

        [HttpPost]
        public ActionResult NewDeveloper([FromBody] Developer developer)
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
        public ActionResult Edit(Developer developer)
        {
            developers.Update(developer);
            return Ok();
        }

        [HttpDelete]
        [Route("/api/projects/{projectId}/d/{id}")]
        public ActionResult QuitProject(int projectId, int id)
        {
            var project = projects.GetItem(projectId);
            if (project == null)
            {
                return NotFound("Project not found: " + projectId);
            }

            var developer = developers.GetItem(id);
            if (developer == null)
            {
                return NotFound("Developer does not exist: " + id);
            }
             
            project.Developers.Remove(
                project.Developers.FirstOrDefault(item => item.Id == id)
            );

            projects.Update(project);

            return Ok();
        }

        [HttpPost]
        [Route("/api/projects/{projectId}/d/{id}")]
        public ActionResult AssignToProject(int projectId, int id)
        {
            var project = projects.GetItem(projectId);
            if (project == null)
            {
                return NotFound("Project not found: " + projectId);
            }

            var developer = developers.GetItem(id);
            if (developer == null)
            {
                return NotFound("Developer does not exist: " + id);
            }

            /*
            if (project.Developers == null)
            {
                project.Developers = new List<ProjectDeveloperEntity>();
            }

            if (!project.Developers.Any(item => item.DeveloperId == id))
            {
                project.Developers.Add(new ProjectDeveloperEntity()
                {
                    DeveloperId = developer.Id,
                    ProjectId = project.Id
                });
                projects.Update(project);
            }*/

            return Ok();
        }
    }
}