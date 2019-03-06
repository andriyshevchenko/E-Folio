using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eFolio.API.Models;
using eFolio.DTO.Common;
using eFolio.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace eFolio.API.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/users")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        // private readonly IEmailSender _emailSender;

        public AccountController(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
            // _emailSender = emailSender;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegister model)
        {
            try
            {
                var user = new UserEntity { UserName = model.Email, Email = model.Email};

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var createUser = await _userManager.FindByEmailAsync(model.Email);

                    //string confirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(createUser).Result;
                    //string confirmationLink = $"{Request.Headers["Origin"].ToString()}/emailverification?email={user.Email}&token={confirmationToken}";

                    //var message = "<div style=\"width: 640px\"><table><tr><td>Please confirm your email address by clicking the link below:</td></tr>"
                    //              + $"<td><a href=\"{confirmationLink}\">Click here  </a></td><tr><td></td></tr>"
                    //              + "<tr> <td>Or you can copy and paste this link into your browser: </td></tr>"
                    //              + $"<tr><td style=\"padding:15px;background-color:#e2e2e2e2;font-style: italic;\"><p style=\"width:640px;\"><a style=\"color:black;cursor:auto;\" href=\"#\">{confirmationLink}</a></p></tr></table></div>";

                    // await _emailSender.SendEmailAsync(user.Email, "Email confirmation", message);

                    return Ok(true);
                }

                return StatusCode((int) HttpStatusCode.BadRequest, new ErrorResponse(result.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorResponse(ex));
            }
        }
    }
}
