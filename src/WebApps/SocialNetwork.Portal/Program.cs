namespace SocialNetwork.Portal;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseAuthorization();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapGet("/.well-known/acme-challenge/6-HJofuQIiI-uPZC8td1XS4-6mCLPLGhg7Oj7ei6UiA", () =>
        {
            return "6-HJofuQIiI-uPZC8td1XS4-6mCLPLGhg7Oj7ei6UiA.QW_WgChS-Jy0OazS-HA49AoTpScnZpEFNVj4JBlJCns";
        });

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}