using System;
using System.Collections.Generic;
using System.Linq;

using eFolio.DTO.Common;
using eFolio.BL;
using eFolio.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using eFolio.API.Models;
using eFolio.DTO;

namespace eFolio.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DevelopersController : ControllerBase
    { 
        private IProjectService _projectService;
        private IDeveloperService _developerService;
        private ILogger _logger;

        public DevelopersController(IProjectService projectService,
                                    IDeveloperService developerService,
                                    ILogger<DevelopersController> logger)
        { 
            this._projectService = projectService;
            this._developerService = developerService;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult GetDevelopers()
        {
            try
            {
                return Ok(_developerService.GetItemsList());
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }

        [HttpGet("search/{request}")]
        public IActionResult SearchDevelopers(string request, [FromQuery] int from, [FromQuery] int size)
        {
            try
            {
                return Ok(_developerService.Search(request, new Paging(from, size)));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }
 
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetDeveloper(int id)
        {
            try
            {
                var project = _developerService.GetItem(id);
                if (project == null)
                {
                    return NotFound(id);
                }
                return Ok(project);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }

        [HttpPost]
        public IActionResult NewDeveloper([FromBody] Developer developer)
        {
            try
            {
                _developerService.Add(developer);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDeveloper(int id)
        {
            try
            {
                _developerService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }

        [HttpPut]
        public IActionResult Edit(Developer developer)
        {
            try
            {
                _developerService.Update(developer);
                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }


        }

        [HttpDelete("{projectId}/d/{id}")]
        public IActionResult QuitProject(int projectId, int id)
        {
            try
            {
                var project = _projectService.GetItem(projectId);
                if (project == null)
                {
                    return NotFound("Project not found: " + projectId);
                }

                var developer = _developerService.GetItem(id);
                if (developer == null)
                {
                    return NotFound("Developer does not exist: " + id);
                }

                project.Developers.Remove(
                    project.Developers.FirstOrDefault(item => item.Id == id)
                );

                _projectService.Update(project);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }  
        }

        [HttpPut("{projectId}/d/{id}")]
        public IActionResult AssignToProject(int projectId, int id)
        {
            try
            {
                var project = _projectService.GetItem(projectId);
                if (project == null)
                {
                    return NotFound("Project not found: " + projectId);
                }

                var developer = _developerService.GetItem(id);
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
                    _projectService.Update(project);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }
    }
}