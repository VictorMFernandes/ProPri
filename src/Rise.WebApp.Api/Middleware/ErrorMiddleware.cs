using Microsoft.AspNetCore.Http;
using Rise.WebApp.Api.Helpers;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Rise.WebApp.Api.Middleware
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var resultado = new ApiResponse(false, null, new
            {
                message = exception.Message,
                internError = exception.InnerException?.Message,
                stackTrace = exception.StackTrace
            });

            return context.Response.WriteAsync(resultado.ToString());
        }
    }
}