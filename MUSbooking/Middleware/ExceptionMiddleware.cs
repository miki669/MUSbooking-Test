using System.Net;
using MUSbooking.Exceptions;
using MUSbooking.Model;
using Newtonsoft.Json;

namespace MUSbooking.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        ErrorDetails errorDetails;
        context.Response.StatusCode = exception switch
        {
            NotFoundException => (int)HttpStatusCode.NotFound,
            OrderNotFoundException => (int)HttpStatusCode.NotFound,
            OrderBadRequestException => (int)HttpStatusCode.BadRequest,
            BadRequestException => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };
        errorDetails = new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message
        };

        return context.Response.WriteAsync(JsonConvert.SerializeObject(errorDetails));
    }
}
