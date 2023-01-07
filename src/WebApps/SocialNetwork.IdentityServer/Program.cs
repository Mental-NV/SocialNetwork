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

            if (!string.IsNullOrWhiteSpace(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]))
            {
                builder.Services.AddApplicationInsightsTelemetry();
            }

            var app = builder.Build();

            ILogger<Program> logger = app.Services.GetRequiredService<ILogger<Program>>();

            string basePath = builder.Configuration["BasePath"];
            logger.LogInformation("BasePath: {basePath}", basePath);
            if (!string.IsNullOrWhiteSpace(basePath))
            {
                app.UsePathBase(basePath);
            }

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Lax
            });

            app.UseDeveloperExceptionPage();
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
}