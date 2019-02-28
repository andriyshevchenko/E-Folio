using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using eFolio.DTO;
using eFolio.BL;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using eFolio.API.Models;

namespace eFolio
{
    [Route("api/projects")]
    [Produces("application/json")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;
        private ILogger _logger;

        public ProjectController(IProjectService projectService,
            ILogger<ProjectController> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetProjects()
        {
            try
            {
                return Ok(_projectService.GetItemsList());
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorResponse(ex));
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<Project>> GetProject(int id)
        {
            try
            {
                var project = _projectService.GetItem(id);
                if (project == null)
                {
                    return NotFound(id);
                }
                return Ok(project);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int) HttpStatusCode.BadRequest, new ErrorResponse(ex));
            }
        }

        [HttpDelete]
        [Route("{id}")]
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
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorResponse(ex));
            }
        }

        [HttpPost]
        public ActionResult MakeNewProject([FromBody] Project project)
        {
            try
            {
                _projectService.Add(project);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);

                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorResponse(ex));
            }


        }
         
        [HttpPut]
        public ActionResult Edit([FromBody] Project project)
        {
            try
            {
                _projectService.Update(project);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorResponse(ex));
            }
        }
    }
}