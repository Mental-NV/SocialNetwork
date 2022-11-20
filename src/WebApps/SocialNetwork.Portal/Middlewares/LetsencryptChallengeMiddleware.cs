namespace SocialNetwork.Portal.Middlewares;

public class LetsencryptChallengeMiddleware
{
    private readonly RequestDelegate next;

    public LetsencryptChallengeMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
    {
        string path = configuration["LetscryptChallendge:Path"];
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ApplicationException("Configuration 'LetscryptChallendge:Path' cannot be empty");
        }
        string content = configuration["LetscryptChallendge:Content"];
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ApplicationException("Configuration 'LetscryptChallendge:Content' cannot be empty");
        }

        if (!context.Request.IsHttps && string.Equals(context.Request.Method, "GET", StringComparison.InvariantCultureIgnoreCase))
        {
            if (context.Request.Path == path)
            {
                await context.Response.WriteAsync(content);
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