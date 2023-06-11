using AspAuth.Interfaces;
using AspAuth.Services;

namespace AspAuth.Extensions
{
    public static class ServiceExtenions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection service)
        {
            service.AddScoped<IAuthService, AuthService>();

            return service;
        }
    }
}
