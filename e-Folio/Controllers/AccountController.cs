using eFolio.API.Models;
using eFolio.DTO.Common;
using eFolio.EF;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eFolio.API.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/users")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        // private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        // private readonly SignInManager<UserEntity> _signInManager;

        public AccountController(UserManager<UserEntity> userManager,
                                 IConfiguration configuration)
                                // SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            // _emailSender = emailSender;
            _configuration = configuration;
           // _signInManager = signInManager;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var tokenResponse = await RequestTokenAsync(model.Email, model.Password);

                    if (tokenResponse.HttpStatusCode == HttpStatusCode.OK)
                        return Ok(new
                        {
                            tokenResponse.AccessToken,
                            tokenResponse.ExpiresIn,
                            tokenResponse.TokenType,
                            tokenResponse.RefreshToken,
                        });
                    else
                        throw new Exception($"{tokenResponse.Error}{Environment.NewLine}{tokenResponse.ErrorDescription}");
                }

                return StatusCode((int)HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorResponse(ex));
            }
        } 

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegister model)
        {
            try
            {
                var user = new UserEntity
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = model.ConfirmedEmail = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim("role", "user"));
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

        public async Task<TokenResponse> RequestTokenAsync(string username, string password)
        {         
            var discoveryClient = new DiscoveryClient(_configuration["ApiBaseUrl"]);
            var doc = await discoveryClient.GetAsync();

            var tokenClient = new TokenClient(doc.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestAsync(new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", username},
                {"password", password}, 
            });

            return tokenResponse;
        }

        //public async Task<TokenResponse> RequestRefreshTokenAsync(string refreshToken)
        //{
        //    var discoveryClient = new DiscoveryClient(_configuration["ApiBaseUrl"]);
        //    var doc = await discoveryClient.GetAsync();
        //    var tokenClient = new TokenClient(doc.TokenEndpoint, "ro.client", "secret");

        //    return await tokenClient.RequestRefreshTokenAsync(refreshToken);
        //}
    }
}
