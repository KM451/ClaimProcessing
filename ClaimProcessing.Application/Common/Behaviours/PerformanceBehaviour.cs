using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ClaimProcessing.Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;
        private readonly Stopwatch _timer;
        public PerformanceBehaviour(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();   
            _logger = logger;    
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsed = _timer.ElapsedMilliseconds;

            if (elapsed > 500) 
            { 
                var requestName = typeof(TRequest).Name;

                _logger.LogInformation("ClaimProcessing Long Running Request: {Name} ({elapsed} milliseconds) {@Request}", requestName, elapsed, request);
            }

            return response;
        }
    }
}
