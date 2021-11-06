using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Airport.Api.Middlewares
{
    /// <summary>
    /// Error handler for error when send and get grpc request.
    /// </summary>
    public class ErrorHandlingGrpc : Interceptor
    {
        private readonly ILogger<ErrorHandlingGrpc> _logger;

        public ErrorHandlingGrpc(ILogger<ErrorHandlingGrpc> logger)
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
                throw;
            }
        }
    }
}
