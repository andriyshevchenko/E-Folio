using System.Collections.Generic;
using System.Linq;

using eFolio.DTO;
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
        private IProjectService projectService;
        private IDeveloperService developerService;

        public DevelopersController(IProjectService projectService,
                                    IDeveloperService developerService)
        { 
            this.projectService = projectService;
            this.developerService = developerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Developer>> GetDevelopers()
        {
            return Ok(developerService.GetItemsList());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<Developer>> GetDeveloper(int id)
        {
            var project = developerService.GetItem(id);
            if (project == null)
            {
                return NotFound(id);
            }
            return Ok(project);
        }

        [HttpPost]
        public ActionResult NewDeveloper([FromBody] Developer developer)
        {
            developerService.Add(developer);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteDeveloper(int id)
        {
            developerService.Delete(id);
            return Ok();
        }

        [HttpPut]
        public ActionResult Edit(Developer developer)
        {
            developerService.Update(developer);
            return Ok();
        }

        [HttpDelete]
        [Route("/api/projects/{projectId}/d/{id}")]
        public ActionResult QuitProject(int projectId, int id)
        {
            var project = projectService.GetItem(projectId);
            if (project == null)
            {
                return NotFound("Project not found: " + projectId);
            }

            var developer = developerService.GetItem(id);
            if (developer == null)
            {
                return NotFound("Developer does not exist: " + id);
            }
             
            project.Developers.Remove(
                project.Developers.FirstOrDefault(item => item.Id == id)
            );

            projectService.Update(project);

            return Ok();
        }

        [HttpPut]
        [Route("/api/projects/{projectId}/d/{id}")]
        public ActionResult AssignToProject(int projectId, int id)
        {
            var project = projectService. GetItem(projectId);
            if (project == null)
            {
                return NotFound("Project not found: " + projectId);
            }

            var developer = developerService.GetItem(id);
            if (developer == null)
            {
                return NotFound("Developer does not exist: " + id);
            } 

            if (project.Developers == null)
            {
                project.Developers = new List<Developer>();
            }
    
            if (!project.Developers.Any(item => item.Id == id))
            {
                project.Developers.Add(developer);
                projectService.Update(project);
            }

            return Ok();
        }
    }
}