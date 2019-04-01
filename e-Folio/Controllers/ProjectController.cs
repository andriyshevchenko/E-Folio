using eFolio.API.Models;
using eFolio.BL;
using eFolio.DTO;
using eFolio.DTO.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using eFolio.Attibutes;

namespace eFolio.Api.Controllers
{
    public class RequestBody<T>
    {
        public T Item { get; set; }
    }

    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private static string default_options = "screenshots,developers";
        private IProjectService _projectService;
        private ILogger _logger;

        public ProjectController(
            IProjectService projectService,
            ILogger<ProjectController> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        [HttpGet]
        [AnonymousOrHasClaim("role", "admin", "sale", "user")]
        public IActionResult GetProjects()
        {
            try
            {
                return Ok(_projectService.GetItemsList(DescriptionKind.External));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }

        [HttpGet("search/{request}")]
        [AnonymousOrHasClaim("role", "admin", "sale", "user")]
        public IActionResult SearchProjects(string request, [FromQuery] int from, [FromQuery] int size)
        {
            try
            {
                return Ok(_projectService.Search(request, new Paging(from, size), DescriptionKind.External));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }

        [HttpGet("{id}")]  
        [AnonymousOrHasClaim("role", "admin", "sale", "user")]
        public IActionResult GetProject(int id, string options)
        {
            try
            {
                var project = _projectService.GetItem(id, DescriptionKind.None, (options ?? default_options).Split(','));
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

        [HttpDelete("{id}")]  
        [HasClaim("role", "admin", "sale")]
        public ActionResult DeleteProject(int id)
        {
            try
            {
                _projectService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }

        [HttpPost] 
        [HasClaim("role", "admin", "sale")]
        public IActionResult MakeNewProject([FromBody] Project project)
        {
            try
            {
                _projectService.Add(project);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);

                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }     
        }

        [HttpPut("{project}/details")] 
        [HasClaim("role", "admin", "sale")]
        public IActionResult EditDetails(int project, [FromBody] Context details)
        {
            try
            {
                _projectService.UpdateDetails(project, details);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }

        [HttpDelete("{project}/screenshots")]  
        [HasClaim("role", "admin", "sale") ]
        public IActionResult DeleteScreenshots(int project, [FromBody] RequestBody<int[]> deleted)
        {
            try
            {
                _projectService.DeleteScreeenshots(project, deleted.Item);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }

        [HttpPut("{project}/screenshots")] 
        [HasClaim("role", "admin", "sale")]
        public IActionResult UpdateScreenshots(int project, [FromBody] Dictionary<int, FolioFile> files)
        {
            try
            {
                _projectService.UpdateScreenshots(project, files);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }

        [HttpPut] 
        [HasClaim("role", "admin", "sale")]
        public IActionResult Edit([FromBody] Project project)
        {
            try
            {
                _projectService.Update(project);
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