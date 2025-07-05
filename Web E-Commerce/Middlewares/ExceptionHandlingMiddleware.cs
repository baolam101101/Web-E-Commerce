//using System.Net;
//using System.Text.Json;
//using Microsoft.AspNetCore.Http;
//using Web_E_Commerce.DTOs.Response;

//namespace Web_E_Commerce.Middlewares
//{
//    public class ExceptionHandlingMiddleware(RequestDelegate next)
//    {
//        public async Task InvokeAsync(HttpContext context)
//        {
//            try
//            {
//                await next(context);
//            }
//            catch (Exception ex)
//            {
//                await HandleExceptionAsync(context, ex);
//            }
//        }

//        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
//        {
//            var statusCode = (int)HttpStatusCode.InternalServerError;

//            var errorResponse = new ErrorResponse
//            (
//                message: "Internal Server Error",
//                statusCode: statusCode,
//                details: exception.Message,
//                code: "INTERNAL_ERROR"
//            );

//            var json = JsonSerializer.Serialize(errorResponse);

//            context.Response.ContentType = "application/json";
//            context.Response.StatusCode = statusCode;

//            return context.Response.WriteAsync(json);
//        }
//    }

//    public static class ExceptionHandlingMiddlewareExtensions
//    {
//        public static IApplicationBuilder UseCustomExceptionHandling(this IApplicationBuilder builder)
//        {
//            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
//        }
//    }
//}
