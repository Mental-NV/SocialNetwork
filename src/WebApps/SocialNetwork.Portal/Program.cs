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

            app.MapGet("/", (HttpContext httpContext, IConfiguration configuration) =>
            {
                string? hostName = configuration["HOSTNAME"];
                return $"SocialNetwork.Portal. Redeploy with a new subsciprtion. Pod hostname: {hostName}";
            });

            app.MapGet("/.well-known/acme-challenge/MWpWlZKdpCFBIQY88rwW1SDZclVJb1OTsYQM8Pb6bwY", () =>
            {
                return "MWpWlZKdpCFBIQY88rwW1SDZclVJb1OTsYQM8Pb6bwY.QW_WgChS-Jy0OazS-HA49AoTpScnZpEFNVj4JBlJCns";
            });

            app.Run();
        }
    }
}