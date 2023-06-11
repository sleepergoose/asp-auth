using AspAuth.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AspAuth.Extensions
{
    public static class AuthExtensions
    {
        public static AuthenticationBuilder AddAuthService(this IServiceCollection services)
        {
            return services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Cookie.Name = "asp_auth_demo";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    options.SlidingExpiration = true;
                    options.Events.OnRedirectToLogin = (context) =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                    options.Cookie.HttpOnly = true;
                    // options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                });
        }

        public static IServiceCollection AddAuthorizationService(this IServiceCollection services)
        {
            return services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.Policies.Admin, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
                    policy.RequireClaim(Claims.Claims.Role, Role.Admin.ToString());
                });

                options.AddPolicy(Policies.Policies.User, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
                    policy.RequireClaim(Claims.Claims.Role, new[] { Role.Admin.ToString(), Role.User.ToString() });
                });

                options.DefaultPolicy = options.GetPolicy(Policies.Policies.User)!;
            });
        }
    }
}
