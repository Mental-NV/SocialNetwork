using Microsoft.Net.Http.Headers;
using SocialNetwork.Portal.ApiServices;
using SocialNetwork.Portal.Middlewares;

namespace SocialNetwork.Portal;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var services = builder.Services;
        services
            .AddAuthorization()
            .AddControllersWithViews()
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
        services.AddHttpClient("ProfileApiClient", (services, client) =>
        {
            IConfiguration configuration = services.GetRequiredService<IConfiguration>();
            var profileApiBaseUrlString = configuration["ApiServices:ProfileApiBaseUrl"];
            if (string.IsNullOrEmpty(profileApiBaseUrlString))
            {
                throw new ApplicationException($"ProfileApi base url cannot be null");
            }
            client.BaseAddress = new Uri(profileApiBaseUrlString);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        });
        services.AddScoped<IProfileApiService, ProfileApiService>();

        var app = builder.Build();

        app.UseDeveloperExceptionPage();
        app.UseLetsencryptChallenge();

        app.UseAuthorization();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}