using MediatR;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using WRSoftware.Utils.Logging;

namespace WRSoftware.Utils.IoC.Framework.Behaviours
{
    /// <summary>
    /// The Performance Behaviour, that will log the velocity of each request
    /// if this is configured
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="MediatR.IPipelineBehavior{TRequest, TResponse}" />
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        /// <summary>
        /// The logger configs
        /// </summary>
        private readonly LoggerConfig _loggerConfigs;

        /// <summary>
        /// The timer
        /// </summary>
        private readonly Stopwatch _timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceBehaviour{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="loggerConfig">The logger configuration.</param>
        /// <exception cref="System.ArgumentNullException">loggerConfig</exception>
        public PerformanceBehaviour(Func<object, LoggerConfig> loggerConfig)
        {
            _timer = new Stopwatch();

            if (loggerConfig == null)
                throw new ArgumentNullException(nameof(loggerConfig));
            this._loggerConfigs = loggerConfig((object)this);
        }

        /// <summary>
        /// Pipeline handler. Perform any additional behavior and await the <paramref name="next" /> delegate as necessary
        /// </summary>
        /// <param name="request">Incoming request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler.</param>
        /// <returns>
        /// Awaitable task returning the <typeparamref name="TResponse" />
        /// </returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;
            //TODO: virar configuraçao no nlog
            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;
                ///var userId = _currentUserService.UserId ?? string.Empty;
                ///var userName = string.Empty;

                ///if (!string.IsNullOrEmpty(userId))
                ///{
                ///    userName = await _identityService.GetUserNameAsync(userId);
                ///}
                LoggerSingleton.Warn(_loggerConfigs,
                    $"Long Running Request: Request: {requestName} - {elapsedMilliseconds} milliseconds");
            }

            return response;
        }
    }
}
