using NLog;
using ILogger = NLog.ILogger;

namespace ECommerceWebhook.Api.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger _logger;
    
    public ErrorHandlingMiddleware(ILogger logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (ArgumentException e)
        {
            _logger.Info(e, e.Message);
            
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong :(");
        }
    }
}