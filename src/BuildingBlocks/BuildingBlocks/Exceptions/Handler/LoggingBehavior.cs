using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Log request details (Pre-processing)
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation($"Handling {requestName} - Request: {JsonSerializer.Serialize(request)}");

            var startTime = DateTime.UtcNow;

            try
            {
                // Execute next step in pipeline
                var response = await next();

                // Log response and execution time (Post-processing)
                var elapsedTime = DateTime.UtcNow - startTime;
                _logger.LogInformation($"Handled {requestName} - Response: {JsonSerializer.Serialize(response)} - TimeTaken: {elapsedTime.TotalMilliseconds}ms");

                return response;
            }
            catch (Exception ex)
            {
                // Log exceptions
                _logger.LogError(ex, $"Error handling {requestName} - Request: {JsonSerializer.Serialize(request)}");
                throw; // Re-throw exception for further processing
            }
        }
    }
}
