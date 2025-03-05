using System.Net;
using System.Text.Json;
using Okkam.Cars.Core.Exceptions;

namespace Okkam.Cars.WebApi;

/// <summary>
/// Глобальный обработчик ошибок.
/// </summary>
public class GlobalErrorHandlingMiddleware
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    /// <summary>
    /// Глобальный обработчик ошибок.
    /// </summary>
    /// <param name="next">Делегат функции обрабатывающей запрос.</param>
    /// <param name="logger">Логгер.</param>
    public GlobalErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    /// <summary>
    /// Выполняет запрос.
    /// </summary>
    /// <param name="context">Контекст http.</param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
            _logger.LogError(ex, ex.Message);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode status;
        var stackTrace = string.Empty;
        string message;

        var exceptionType = exception.GetType();

        if (exceptionType == typeof(HttpRequestException))
        {
            var httpException = (HttpRequestException)exception;
            message = exception.Message;
            status = httpException.StatusCode ?? new HttpStatusCode();
            stackTrace = exception.StackTrace;
        }

        else if (exceptionType == typeof(NotImplementedException))
        {
            status = HttpStatusCode.NotImplemented;
            message = exception.Message;
            stackTrace = exception.StackTrace;
        }
        else if (exceptionType == typeof(UnauthorizedAccessException))
        {
            status = HttpStatusCode.Unauthorized;
            message = exception.Message;
            stackTrace = exception.StackTrace;
        }
        else if (exceptionType == typeof(ObjectNotFoundException)
                 || exceptionType == typeof(Minio.Exceptions.ObjectNotFoundException))
        {
            status = HttpStatusCode.NotFound;
            message = exception.Message;
            stackTrace = exception.StackTrace;
        }
        else
        {
            status = HttpStatusCode.InternalServerError;
            message = exception.Message;
            stackTrace = exception.StackTrace;
        }

        var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;
        return context.Response.WriteAsync(exceptionResult);
    }
}