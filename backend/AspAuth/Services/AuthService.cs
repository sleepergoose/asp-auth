using AspAuth.Dtos;
using AspAuth.Enums;
using AspAuth.Interfaces;
using System.Security.Claims;

namespace AspAuth.Services
{
    public class AuthService: IAuthService
    {
        public ClaimsPrincipal? GetAuthPrincipal(UserSignInDto dto)
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

                claimsIdentity = new ClaimsIdentity(claims, "TempCookies");
            }
            else if (dto.Email == "user@server.com" && dto.Password == "12345678")
            {
                claims = new List<Claim>
                {
                    new (Claims.Claims.Sub, Guid.NewGuid().ToString()),
                    new (Claims.Claims.Name, "User Name"),
                    new (Claims.Claims.Role, Role.User.ToString()),
                };

                claimsIdentity = new ClaimsIdentity(claims, "TempCookies");
            }
            else if (dto.Email == "guest@server.com" && dto.Password == "12345678")
            {
                claims = new List<Claim>
                {
                    new (Claims.Claims.Guest, "true"),
                };

                claimsIdentity = new ClaimsIdentity(claims, "TempCookies");
            }
            else
            {
                return null;
            }

            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
