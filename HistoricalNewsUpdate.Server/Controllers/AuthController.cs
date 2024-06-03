using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HistoricalNewsUpdate.Common.Models;
using HistoricalNewsUpdate.Models.User;
using HistoricalNewsUpdate.Services;
using HistoricalNewsUpdate.Common;
using System.Security.Claims;

namespace HistoricalNewsUpdate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new ApiSuccessResult<bool>(true));
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            LoginResponse data = await _authService.Login(request);
            if (!string.IsNullOrEmpty(data.UserName))
            {
                var devSpaceUserIdentity = new ClaimsPrincipal();
                var claims = new List<Claim> {
                            new Claim(ClaimType.UserName, data.UserName),
                            new Claim(ClaimType.Permissions, data.Permissions)
                        };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                devSpaceUserIdentity.AddIdentity(identity);

                var authProps = new AuthenticationProperties()
                {
                    IsPersistent = true
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, devSpaceUserIdentity, authProps);

            }
            return Ok(new ApiSuccessResult<LoginResponse>(data));
        }

        private string GetIPAddress()
        {
            string ipAddress = HttpContext.Request.Headers["X-Forwarded-For"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return HttpContext.Connection.RemoteIpAddress?.ToString();
        }
    }
}
