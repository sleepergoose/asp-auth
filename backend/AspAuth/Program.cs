using AspAuth.Claims;
using AspAuth.Enums;
using AspAuth.Policies;
using DotNetEnv;
using DotNetEnv.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography.Xml;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
    .AddDotNetEnv(".env", LoadOptions.TraversePath())
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

builder.Configuration.AddConfiguration(config);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.Admin, policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
        policy.RequireClaim(Claims.Role, Role.Admin.ToString());
    });

    options.AddPolicy(Policies.User, policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
        policy.RequireClaim(Claims.Role, new [] { Role.Admin.ToString(), Role.User.ToString() });
    });

    options.DefaultPolicy = options.GetPolicy(Policies.User)!;
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var origins = app.Configuration.GetSection("AllowedOrigins").Get<string[]>();

app.UseCors(opt => opt
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

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

