using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TrackingCode.Api
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            switch (ex)
            {
                case InvalidOperationException _:
                    if (ex.Source == "Microsoft.EntityFrameworkCore" && ex.Message == "Enumerator failed to MoveNextAsync.")//todo better way if possible
                    {
                        code = HttpStatusCode.NotFound;
                    }
                    break;
                // case EntityNotFoundException _:
                //     code = HttpStatusCode.NotFound;
                //     break;
                // case BusinessException _:
                //     code = HttpStatusCode.BadRequest;
                //     break;
            }

#if DEBUG
            var errorMessage = ex.Message;
#else
            var errorMessage = code == HttpStatusCode.InternalServerError ? "Internal server error!" : ex.Message;
#endif

            var result = JsonConvert.SerializeObject(new { error = errorMessage });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}