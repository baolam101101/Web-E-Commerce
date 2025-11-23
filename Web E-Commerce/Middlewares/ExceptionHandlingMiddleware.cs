using System.Net;
using System.Reflection;
using System.Text.Json;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.DTOs.Shared.Constants;
using Web_E_Commerce.Exceptions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    private static readonly Dictionary<Type, HttpStatusCode> StatusMap = new()
    {
        { typeof(NotFoundException), HttpStatusCode.NotFound },
        { typeof(BadRequestException), HttpStatusCode.BadRequest },
        { typeof(UnauthorizedException), HttpStatusCode.Unauthorized },
        { typeof(ForbiddenException), HttpStatusCode.Forbidden },
        { typeof(ValidationException), HttpStatusCode.BadRequest }
    };

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception caught by middleware");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var exceptionType = ex.GetType();

        // Status
        var statusCode = StatusMap.TryGetValue(exceptionType, out var code)
            ? (int)code
            : (int)HttpStatusCode.InternalServerError;

        string messageKey;
        string? description = null;

        if (ex is BaseException baseEx)
        {
            messageKey = baseEx.Message;
            description = baseEx.Description ?? GetDescriptionFromKey(baseEx.Message);
        }
        else
        {
            messageKey = MessageKeys.INTERNAL_SERVER_ERROR;
            description = MessageDescriptions.INTERNAL_SERVER_ERROR;
        }

        // Validation
        if (ex is ValidationException ve)
        {
            description = string.Join("; ", ve.Errors);
        }

        var response = new ErrorResponse
        {
            StatusCode = statusCode,
            Code = messageKey,
            Message = messageKey,
            Description = description
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static string? GetDescriptionFromKey(string key)
    {
        var field = typeof(MessageDescriptions).GetField(
            key,
            BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase
        );

        return field?.GetValue(null)?.ToString();
    }
}