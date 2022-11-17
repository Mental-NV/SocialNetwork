using Microsoft.AspNetCore.Http.Features;

namespace SocialNetwork.Portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.MapGet("/", (HttpContext httpContext, IConfiguration configuration) =>
            {
                string? hostName = configuration["HOSTNAME"];
                return $"SocialNetwork.Portal. Renewed SSL certificate. Pod hostname: {hostName}";
            });

            app.MapGet("/.well-known/acme-challenge/6-HJofuQIiI-uPZC8td1XS4-6mCLPLGhg7Oj7ei6UiA", () =>
            {
                return "6-HJofuQIiI-uPZC8td1XS4-6mCLPLGhg7Oj7ei6UiA.QW_WgChS-Jy0OazS-HA49AoTpScnZpEFNVj4JBlJCns";
            });

            app.Run();
        }
    }
}