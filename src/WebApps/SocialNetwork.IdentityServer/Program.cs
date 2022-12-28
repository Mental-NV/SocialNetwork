using IdentityServer4.Test;

namespace SocialNetwork.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentityServer()
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryApiScopes(Config.ApiScopes)
                //.AddInMemoryIdentityResources(Config.IdentityResources)
                .AddTestUsers(TestUsers.Users)
                .AddDeveloperSigningCredential();

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.Secure = CookieSecurePolicy.SameAsRequest;
                options.OnAppendCookie = cookieContext =>
                    AuthenticationHelpers.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext =>
                    AuthenticationHelpers.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            });

            var app = builder.Build();

            ILogger<Program> logger = app.Services.GetRequiredService<ILogger<Program>>();

            string basePath = builder.Configuration["BasePath"];
            logger.LogInformation("BasePath: {basePath}", basePath);
            if (!string.IsNullOrWhiteSpace(basePath))
            {
                app.UsePathBase(basePath);
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            /*app.MapGet("/api1", (HttpContext ctx, LinkGenerator link)
                    => $"API1: PathBase: {ctx.Request.PathBase} Path: {ctx.Request.Path} Link: {link.GetPathByName(ctx, "api2", values: null)}")
                .WithName("api1");

            app.MapGet("/api2", (HttpContext ctx, LinkGenerator link)
                    => $"API2: PathBase: {ctx.Request.PathBase} Path: {ctx.Request.Path} Link: {link.GetPathByName(ctx, "api1", values: null)}")
                .WithName("api2");

            app.MapFallback((HttpContext ctx, LinkGenerator link)
                    => $"FALLBACK: PathBase: {ctx.Request.PathBase} Path: {ctx.Request.Path} Link: {link.GetPathByName(ctx, "api1", values: null)}");
            */
            app.Run();
        }
    }

    public static class AuthenticationHelpers
    {
        public static void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite != SameSiteMode.None)
                return;
            string userAgent = httpContext.Request.Headers["User-Agent"].ToString();
            if (httpContext.Request.IsHttps && !AuthenticationHelpers.DisallowsSameSiteNone(userAgent))
                return;
            options.SameSite = SameSiteMode.Unspecified;
        }

        public static bool DisallowsSameSiteNone(string userAgent) => userAgent.Contains("CPU iPhone OS 12") || userAgent.Contains("iPad; CPU OS 12") || userAgent.Contains("Macintosh; Intel Mac OS X 10_14") && userAgent.Contains("Version/") && userAgent.Contains("Safari") || userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6");
    }
}