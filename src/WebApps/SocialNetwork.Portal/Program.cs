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
                return $"SocialNetwork.Portal. Use latest image. Pod hostname: {hostName}";
            });

            app.Run();
        }
    }
}