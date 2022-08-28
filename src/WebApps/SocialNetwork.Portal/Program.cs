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

            app.MapGet("/", (HttpContext httpContext) =>
            {
                return "SocialNetwork.Portal version AksDeploy";
            });

            app.Run();
        }
    }
}