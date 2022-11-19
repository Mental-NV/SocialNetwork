namespace SocialNetwork.Portal.Middlewares;

public class LetsencryptChallengeMiddleware
{
    private readonly RequestDelegate next;

    public LetsencryptChallengeMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.IsHttps && string.Equals(context.Request.Method, "GET", StringComparison.InvariantCultureIgnoreCase))
        {
            if (context.Request.Path == "/.well-known/acme-challenge/6-HJofuQIiI-uPZC8td1XS4-6mCLPLGhg7Oj7ei6UiA")
            {
                await context.Response.WriteAsync("6-HJofuQIiI-uPZC8td1XS4-6mCLPLGhg7Oj7ei6UiA.QW_WgChS-Jy0OazS-HA49AoTpScnZpEFNVj4JBlJCns");
                return;
            }
        }

        // Call the next delegate/middleware in the pipeline.
        await this.next(context);
    }
}

public static class LetsencryptChallengeMiddlewareExtensions
{
    public static IApplicationBuilder UseLetsencryptChallenge(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LetsencryptChallengeMiddleware>();
    }
}