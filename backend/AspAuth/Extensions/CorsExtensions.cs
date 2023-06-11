namespace AspAuth.Extensions
{
    public static class CorsExtensions
    {
        public static IApplicationBuilder SetCors(this WebApplication app)
        {
            var origins = app.Configuration.GetSection("AllowedOrigins").Get<string[]>();

            return app.UseCors(opt => opt
                .AllowAnyHeader()
                .WithMethods("GET", "POST", "UPDATE", "DELETE")
                .AllowCredentials()
                .SetIsOriginAllowed(origin =>
                {
                    if (string.IsNullOrWhiteSpace(origin))
                        return false;

                    if (origins is not null
                        && origins.Any(o => o.ToLower().StartsWith(origin.ToLower())
                            || o.ToLower().Contains(origin.ToLower())))
                        return true;

                    return false;
                }));
        }
    }
}
