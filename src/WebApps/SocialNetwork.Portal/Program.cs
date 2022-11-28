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
        services.AddHttpClient();
        services.AddScoped<IProfileApiService, ProfileApiService>();

        var app = builder.Build();

        app.UseLetsencryptChallenge();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseAuthorization();

        if (!string.IsNullOrWhiteSpace(builder.Configuration["https_port"]))
        {
            app.UseHttpsRedirection();
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}