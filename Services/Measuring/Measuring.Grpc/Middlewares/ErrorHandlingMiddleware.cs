using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Measuring.Grpc.Middlewares
{
    /// <summary>
    /// Handler for exceiptions in grpc service.
    /// </summary>
    public class ErrorHandlingMiddleware : Interceptor
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error thrown by {context.Method}.");

                var metadata = new Metadata
                {
                    { "inner_exception", ex.Message }
                };

                throw new RpcException(new Status(StatusCode.Internal, ex.ToString()), metadata);
            }
        }
    }
}
