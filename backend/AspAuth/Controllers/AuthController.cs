using AspAuth.Claims;
using AspAuth.Dtos;
using AspAuth.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspAuth.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignInAsync(UserSignInDto dto)
        {
            List<Claim> claims;
            ClaimsIdentity claimsIdentity; 

            // Check user credentials in an appropriate way
            if (dto.Email == "admin@server.com" && dto.Password == "12345678")
            {
                claims = new List<Claim>
                {
                    new (Claims.Claims.Sub, Guid.NewGuid().ToString()),
                    new (Claims.Claims.Name, "Admin Name"),
                    new (Claims.Claims.Role, Role.Admin.ToString()),
                };
            }
            else if (dto.Email == "user@server.com" && dto.Password == "12345678")
            {
                claims = new List<Claim>
                {
                    new (Claims.Claims.Sub, Guid.NewGuid().ToString()),
                    new (Claims.Claims.Name, "User Name"),
                    new (Claims.Claims.Role, Role.User.ToString()),
                };
            }
            else
            {
                return StatusCode(4010);
            }

            claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(claimsPrincipal);

            return Ok();
        }

        [HttpGet("sign-out")]
        [Authorize(Policy = Policies.Policies.User)]
        public async Task<IActionResult> SignOutAsync()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
