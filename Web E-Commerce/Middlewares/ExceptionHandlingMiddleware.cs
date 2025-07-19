using System.Net;
using System.Text.Json;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Exceptions;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context); // tiếp tục middleware pipeline
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception caught");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int statusCode;
        string code;
        string message = exception.Message;
        string? description = null;

        // If BaseException then get description
        if (exception is BaseException baseEx)
        {
            description = baseEx.Description;
        }

        switch (exception)
        {
            case NotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                code = "NOT_FOUND";
                break;

            case BadRequestException:
                statusCode = (int)HttpStatusCode.BadRequest;
                code = "BAD_REQUEST";
                break;

            case UnauthorizedException:
                statusCode = (int)HttpStatusCode.Unauthorized;
                code = "UNAUTHORIZED";
                break;

            case ForbiddenException:
                statusCode = (int)HttpStatusCode.Forbidden;
                code = "FORBIDDEN";
                break;

            case ValidationException ve:
                statusCode = (int)HttpStatusCode.BadRequest;
                code = "VALIDATION_ERROR";
                message = string.Join("; ", ve.Errors);
                break;

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                code = "INTERNAL_SERVER_ERROR";
                break;
        }

        var response = new ErrorResponse(statusCode, code, message, description);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
