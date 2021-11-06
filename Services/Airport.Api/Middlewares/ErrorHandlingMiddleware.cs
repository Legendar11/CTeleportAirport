using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace Airport.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
                _logger.LogError(ex, ex.ToString());

                if (ex is RpcException exRpc)
                    await HandleRpcExceptionAsync(context, exRpc);
                else
                    await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var message = JsonSerializer.Serialize(
                new ApiException("Inner internal error", exception.Message));
            return context.Response.WriteAsync(message);
        }

        private static Task HandleRpcExceptionAsync(HttpContext context, RpcException exception)
        {
            var innerException = exception.Trailers.GetValue("inner_exception");
            var httpException = exception.Trailers.GetValue("http_exception");

            var httpCode = int.TryParse(httpException, out var exCode)
                ? exCode
                : (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = httpCode;

            var message = JsonSerializer.Serialize(
                new ApiException("Part of request processing was failed", innerException ?? exception.Message));
            return context.Response.WriteAsync(message);
        }

        internal record ApiException(
            string title,
            string error
        );
    }
}
