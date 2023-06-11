using AspAuth.Dtos;
using System.Security.Claims;

namespace AspAuth.Interfaces
{
    public interface IAuthService
    {
        ClaimsPrincipal? GetAuthPrincipal(UserSignInDto dto);
    }
}
