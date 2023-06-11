using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [Authorize(Policy = Policies.Policies.User)]
        [HttpGet("claims")]
        public async Task<IActionResult> GetClaimsAsync()
        {
            var userClaims = await Task.Run(() => User.Claims.Select(x => new { name = x.Type, value = x.Value }));

            return Ok(userClaims);
        }

        [Authorize(Policy = Policies.Policies.Admin)]
        [HttpGet("admin-data")]
        public async Task<IActionResult> GetAdminDataAsync()
        {
            return Ok(await Task.Run(() => new { data = "Just admin data" }));
        }

        [Authorize(AuthenticationSchemes = "TempCookies", Policy = Policies.Policies.Guest)]
        [HttpGet("guest-data")]
        public async Task<IActionResult> GetGuestDataAsync()
        {
            return Ok(await Task.Run(() =>
            {
                HttpContext.SignOutAsync("TempCookies");

                return new { data = "Just GUEST data" };
            }));
        }
    }
}
