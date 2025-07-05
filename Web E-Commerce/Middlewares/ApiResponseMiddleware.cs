//using System.Text.Json;

//namespace Web_E_Commerce.Middlewares
//{
//    public class ApiResponseMiddleware(RequestDelegate next)
//    {
//        private readonly RequestDelegate _next = next;

//        public async Task Invoke(HttpContext context)
//        {
//            // Nếu không phải response JSON thì bỏ qua
//            if (!context.Request.Path.StartsWithSegments("/api") ||
//                context.Request.Method == HttpMethods.Options)
//            {
//                await _next(context);
//                return;
//            }

//            var originalBodyStream = context.Response.Body;

//            using var responseBody = new MemoryStream();
//            context.Response.Body = responseBody;

//            await _next(context);

//            context.Response.Body.Seek(0, SeekOrigin.Begin);
//            var bodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
//            context.Response.Body.Seek(0, SeekOrigin.Begin);

//            // Nếu đã là ApiResponse hoặc lỗi thì không wrap lại
//            if (context.Response.ContentType?.Contains("application/json") == true &&
//                !bodyText.StartsWith("{\"message\":") && context.Response.StatusCode == 200)
//            {
//                var wrapped = JsonSerializer.Serialize(new
//                {
//                    message = "Success",
//                    data = JsonSerializer.Deserialize<object>(bodyText)
//                });

//                context.Response.Body = originalBodyStream;
//                context.Response.ContentType = "application/json";
//                await context.Response.WriteAsync(wrapped);
//            }
//            else
//            {
//                context.Response.Body.Seek(0, SeekOrigin.Begin);
//                await responseBody.CopyToAsync(originalBodyStream);
//            }
//        }
//    }
//}

