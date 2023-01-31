using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _rd;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _hostEnvironment;

    public ExceptionMiddleware(RequestDelegate rd, ILogger<ExceptionMiddleware> logger,
        IHostEnvironment hostEnvironment)
    {
        _rd = rd;
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _rd(context);
        }
        catch (Exception e)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            _logger.LogError(e,e.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = _hostEnvironment.IsDevelopment()
                ? new ApiException(statusCode, e.Message, e.StackTrace)
                : new ApiException(statusCode);

            var jsonResponse =  JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}