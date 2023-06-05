using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspAuth.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("claims")]
        public async Task<IActionResult> GetClaimsAsync()
        {
            var userClaims = await Task.Run(() => User.Claims.Select(x => new { name = x.Type, value = x.Value }));

            return Ok(userClaims);
        }
    }
}
