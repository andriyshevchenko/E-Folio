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
            _logger.LogWarning(new Exception(), string.Empty);
  
            return Ok(_projectService.GetItemsList());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<Project>> GetProject(int id)
        {
            Project project;
            try
            {
                project = _projectService.GetItem(id);
                if (project == null)
                {
                    return NotFound(id);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.BadRequest, new ErrorResponse(ex));
            }

            _logger.LogWarning(new Exception(), string.Empty);

            return Ok(project);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteProject(int id)
        {
            try
            {
                _projectService.Delete(id);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorResponse(ex));
            }

            _logger.LogWarning(new Exception(), string.Empty);

            return Ok();
        }

        [HttpPost]
        public ActionResult MakeNewProject([FromBody] Project project)
        {
            try
            {
                _projectService.Add(project);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorResponse(ex));
            }

            _logger.LogWarning(new Exception(), string.Empty);

            return Ok();
        }
         
        [HttpPut]
        public ActionResult Edit([FromBody] Project project)
        {
            try
            {
                _projectService.Update(project);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorResponse(ex));
            }

            _logger.LogWarning(new Exception(), string.Empty);

            return Ok();
        }
    }
}