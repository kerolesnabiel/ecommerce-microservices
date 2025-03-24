using ProductService.Exceptions;

namespace ProductService.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(context, 
                new (400, ex.Errors.Select(e => 
                    new ErrorDetails(e.ErrorMessage, e.PropertyName))));
        }
        catch (UnauthorizedException ex)
        {
            await HandleExceptionAsync(context, new ErrorResponse(401,
                [new(ex.Message, ex.Source?.ToString() ?? "Unknown")]));
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, new ErrorResponse(500, 
                [ new(ex.Message, ex.Source?.ToString() ?? "Unknown")]));
        }
    }

    private Task HandleExceptionAsync(HttpContext context,ErrorResponse response)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.StatusCode;
        response.TraceId = context.TraceIdentifier;
        return context.Response.WriteAsJsonAsync(response);
    }

    private class ErrorResponse(int statusCode, IEnumerable<ErrorDetails> errors)
    {
        public int StatusCode { get; set; } = statusCode;
        public IEnumerable<ErrorDetails> Errors { get; set; } = errors;
        public string TraceId { get; set; } = default!;
    }

    private record ErrorDetails(string message, string field);
}
