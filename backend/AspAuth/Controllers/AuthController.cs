using AspAuth.Dtos;
using AspAuth.Interfaces;
using AspAuth.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspAuth.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignInAsync(UserSignInDto dto)
        {
            var claimsPrincipal = _authService.GetAuthPrincipal(dto);

            if (claimsPrincipal is not null)
            {
                await HttpContext.SignInAsync(claimsPrincipal);

                return Ok();
            }

            return StatusCode(4010);
        }

        [HttpGet("sign-out")]
        [Authorize(Policy = Policies.Policies.User)]
        public async Task<IActionResult> SignOutAsync()
        {
            try
            {
                await HttpContext.SignOutAsync("MainCookies");

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
