using System.Text.Json;

namespace ReviewHubAPI.Middleware;

public class RateLimitResponseMiddleware
{
    private readonly RequestDelegate _next;

    public RateLimitResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == 429) 
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                message = "You have exceeded the rate limit. Please try again later."
            }));

            if (!context.Response.Headers.ContainsKey("Retry-After"))
            {
                context.Response.Headers.Append("Retry-After", "60");  
            }
        }
    }
}
