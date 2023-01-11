using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using SocialNetwork.Portal.ApiServices;
using SocialNetwork.Portal.Middlewares;
using IdentityModel;

namespace SocialNetwork.Portal;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var services = builder.Services;

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
        {
            options.Authority = builder.Configuration["IdentityServer:Authority"];
            options.ClientId = "SocialNetwork.Portal";
            options.ClientSecret = "secret";
            options.ResponseType = "code";
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("address");
            options.Scope.Add("email");
            options.Scope.Add("SocialNetworkAPI");
            options.Scope.Add("roles");
            options.ClaimActions.MapUniqueJsonKey("role", "role");
            options.ClaimActions.MapUniqueJsonKey("address", "address");
            options.SaveTokens = true;
            options.GetClaimsFromUserInfoEndpoint = true;
        });

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

        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}