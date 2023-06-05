using AspAuth.Dtos;
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
            // Check user credentials in an appropriate way
            if (dto.Email == "user@server.com" && dto.Password == "12345678")
            {
                var claims = new List<Claim>
                {
                    new ("sub", Guid.NewGuid().ToString()),
                    new ("name", "Peter Pen"),
                    new ("role", "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);

                return Ok();
            }

            return StatusCode(4010);
        }

        [HttpGet("sign-out")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
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
