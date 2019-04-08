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
using Microsoft.AspNetCore.Identity;
using eFolio.Attibutes;
using System.Threading.Tasks;

namespace eFolio.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController] 
    public class DevelopersController : ControllerBase
    {
        private static readonly string[] haveExtraPermissions = new string[] { "admin", "sales" };

        private IProjectService _projectService;
        private IDeveloperService _developerService;
        private UserManager<UserEntity> _userManager;
        private ILogger _logger;

        public DevelopersController(IProjectService projectService,
                                    IDeveloperService developerService,
                                    UserManager<UserEntity> userManager,
                                    ILogger<DevelopersController> logger)
        {
            this._projectService = projectService;
            this._developerService = developerService;
            this._userManager = userManager;
            this._logger = logger;
        }


        [HttpGet] 
        public async Task<IActionResult> GetDevelopers()
        {
            try
            {
                return base.Ok(await _developerService.GetItemsListAsync(GetCVKindForRequest()));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }

        private CVKind GetCVKindForRequest()
        { 
            return User != null && User.Claims.Any() && haveExtraPermissions.Contains(User.Claims.First().Value) ? 
                CVKind.Internal: 
                CVKind.External;
        }

        [HttpGet("search/{request}")] 
        public async Task<IActionResult> SearchDevelopers(string request, [FromQuery] int from, [FromQuery] int size)
        {
            try
            {
                return Ok(await _developerService.SearchAsync(request, new Paging(from, size), GetCVKindForRequest()));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }

        [HttpGet]
        [Route("{id}")] 
        public async Task<IActionResult> GetDeveloper(int id)
        {
            try
            {
                var project = await _developerService.GetItemAsync(id, GetCVKindForRequest());
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
        [HasClaim("role", "admin")]
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
        [HasClaim("role", "admin")]
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
        [HasClaim("role", "admin")]
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
        [HasClaim("role", "admin", "sales")]
        public async Task<IActionResult> QuitProject(int projectId, int id)
        {
            try
            {
                var project = _projectService.GetItem(projectId, DescriptionKind.External);
                if (project == null)
                {
                    return NotFound("Project not found: " + projectId);
                }

                var developer = await _developerService.GetItemAsync(id, CVKind.External);
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
        [HasClaim("role", "admin", "sales")]
        public async Task<IActionResult> AssignToProject(int projectId, int id)
        {
            try
            {
                var project = _projectService.GetItem(projectId, DescriptionKind.External);
                if (project == null)
                {
                    return NotFound("Project not found: " + projectId);
                }

                var developer = await _developerService.GetItemAsync(id, CVKind.External);
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