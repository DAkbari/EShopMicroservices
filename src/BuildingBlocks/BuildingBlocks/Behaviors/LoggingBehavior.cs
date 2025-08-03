using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse> where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle Request = {request} - Response = {response} - RequestDate = {requestData}", typeof(TRequest).Name, typeof(TResponse).Name, request);
            var timer = Stopwatch.StartNew();
            var response = await next();
            timer.Stop();

            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3)
            {
                logger.LogWarning("[Performance] the request {request} took {timeTaken} seconds", typeof(TRequest).Name, timeTaken.Seconds);
            }

            logger.LogInformation("[END] Handler {request} WITH {response}", typeof(TRequest).Name, typeof(TResponse).Name);
            return response;
        }
    }
}
