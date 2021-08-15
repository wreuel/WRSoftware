using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WRSoftware.Utils.Common.Interfaces;
using WRSoftware.Utils.Logging;

namespace WRSoftware.Utils.IoC.Framework.Behaviours
{
    /// <summary>
    /// The Log Behaviour, that will log the behavior of each request
    /// if this is configured
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="MediatR.IPipelineBehavior{TRequest, TResponse}" />
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        /// <summary>
        /// The logger configs
        /// </summary>
        private readonly LoggerConfig _loggerConfigs;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingBehaviour{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="serviceContext">The service context.</param>
        /// <param name="loggerConfig">The logger configuration.</param>
        /// <exception cref="ArgumentNullException">loggerConfig</exception>
        public LoggingBehaviour(IServiceContext serviceContext, Func<object, IServiceContext, LoggerConfig> loggerConfig)
        {
            if (loggerConfig == null)
                throw new ArgumentNullException(nameof(loggerConfig));
            this._loggerConfigs = loggerConfig((object)this, serviceContext);
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
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response1;
            try
            {
                LoggerSingleton.Debug(this._loggerConfigs, "MediatrHandler::Handling " + typeof(TRequest).FullName);
                TResponse response2 = await next();
                LoggerSingleton.Debug(this._loggerConfigs, "MediatrHandler::Handled " + typeof(TResponse).FullName);
                response1 = response2;
            }
            catch (Exception ex)
            {
                LoggerSingleton.Warn(this._loggerConfigs, ex,
                    "MediatrHandler::An error occurred while handling " + typeof(TRequest).FullName);
                throw;
            }

            return response1;
        }
    }
}
