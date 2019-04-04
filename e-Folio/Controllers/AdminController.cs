using System;
using System.Net;
using eFolio.API.Models;
using eFolio.Attibutes;
using eFolio.BL;
using eFolio.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eFolio.API
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [HasClaim("role", "admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private ILogger _logger;
        private UserManager<UserEntity> _userManager;
        private IAdminService _adminService;
        private AuthDBContext _auth;

        public AdminController(ILogger<AdminController> logger,
                               UserManager<UserEntity> userManager,
                               AuthDBContext auth,
                               IAdminService adminService)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._auth = auth;
            this._adminService = adminService;
        }

        [HttpGet]
        public IActionResult GetUsersList()
        {
            try
            {
                return Ok(_adminService.GetUsersList());
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _adminService.GetUser(id);
                if(user == null)
                {
                    return NotFound(id);
                }
                return Ok(user);
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IActionResult Edit(UserEntity user)
        {
            try
            {
                _adminService.Update(user);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _adminService.Delete(id);
                return Ok();               
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex, string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse(ex));
            }
        }
    }
}
