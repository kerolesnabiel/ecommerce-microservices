using UserService.Domain.Exceptions;

namespace UserService.Presentation.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (IncorrectException ex)
        {
            logger.LogWarning(ex.Message);

            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (InvalidRefreshTokenException ex)
        {
            logger.LogWarning(ex.Message);

            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { error = "Something went wrong" });
        }
    }
}
