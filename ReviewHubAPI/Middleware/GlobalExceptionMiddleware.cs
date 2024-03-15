using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ReviewHubAPI.Middleware;

public class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ArgumentNullException argNullEx)
        {
            _logger.LogWarning(argNullEx, "Argument null exception encountered for request at {Path}", context.Request.Path);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problemDetails = new ProblemDetails
            {
                Title = "Missing argument",
                Detail = argNullEx.Message,
                Status = StatusCodes.Status400BadRequest,
                Instance = context.Request.Path,
                Extensions = { ["traceId"] = System.Diagnostics.Activity.Current?.Id ?? context.TraceIdentifier }
            };

            var jsonResponse = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (NotFoundException notFoundEx)
        {
            _logger.LogWarning(notFoundEx, "NotFound-error on machine: {@Machine} with trace-id: {@TraceId}. Detail: {Detail}",
                               Environment.MachineName, System.Diagnostics.Activity.Current?.Id, notFoundEx.Message);

            await Results.Problem(
                    title: "Resource was not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    extensions: new Dictionary<string, Object?>
                    {
            { "traceId", System.Diagnostics.Activity.Current?.Id },
                    })
                .ExecuteAsync(context);
        }
        catch (ConflictException ex)
        {
            _logger.LogWarning(ex, "Conflict encountered for request at {Path}. Conflict detail: {Message}",
                               context.Request.Path, ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status409Conflict;

            var problemDetails = new ProblemDetails
            {
                Title = "Conflict",
                Detail = ex.Message,
                Status = StatusCodes.Status409Conflict,
                Instance = context.Request.Path,
                Extensions = { ["traceId"] = System.Diagnostics.Activity.Current?.Id ?? context.TraceIdentifier }
            };

            var jsonResponse = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (AuthenticationFailedException authEx)
        {
            _logger.LogWarning(authEx, "Authentication failed for request at {Path}. Detail: {Message}",
                                context.Request.Path, authEx.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            var problemDetails = new ProblemDetails
            {
                Title = "Authentication failed",
                Detail = authEx.Message,
                Status = StatusCodes.Status401Unauthorized,
                Instance = context.Request.Path,
                Extensions = { ["traceId"] = System.Diagnostics.Activity.Current?.Id ?? context.TraceIdentifier }
            };

            var jsonResponse = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request on machine {@Machine} with trace ID {@TraceId}",
                            Environment.MachineName, System.Diagnostics.Activity.Current?.Id);

            await Results.Problem(
                    title: "An unexpected error has occurred",
                    statusCode: StatusCodes.Status500InternalServerError,
                    extensions: new Dictionary<string, Object?>
                    {
            { "traceId", System.Diagnostics.Activity.Current?.Id },
                    })
                .ExecuteAsync(context);
        }
    }
}

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}

public class EmailConflictException : Exception
{
    public EmailConflictException(string message) : base(message) { }
}

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class AuthenticationFailedException : Exception
{
    public AuthenticationFailedException(string message) : base(message) { }
}
