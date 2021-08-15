using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WRSoftware.Utils.Common.Exceptions;
using WRSoftware.Utils.Common.Localization;
using WRSoftware.Utils.Logging;

namespace WRSoftware.Utils.IoC.Framework.Behaviours
{
    /// <summary>
    /// The Validator Behaviour, that will log the comportament of each request
    /// if this is configured
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="MediatR.IPipelineBehavior{TRequest, TResponse}" />
    public class ValidatorBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        /// <summary>
        /// The logger configs
        /// </summary>
        private readonly LoggerConfig _loggerConfigs;

        /// <summary>
        /// The validators
        /// </summary>
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// The localizer
        /// </summary>
        private readonly CommonLocalizationService _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorBehaviour{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="validators">The validators.</param>
        /// <param name="loggerConfig">The logger configuration.</param>
        /// <exception cref="ArgumentNullException">
        /// validators
        /// or
        /// loggerConfig
        /// </exception>
        public ValidatorBehaviour(IEnumerable<IValidator<TRequest>> validators, Func<object, LoggerConfig> loggerConfig, CommonLocalizationService localizer = null)
        {
            this._validators = validators ?? throw new ArgumentNullException(nameof(validators));
            if (loggerConfig == null)
                throw new ArgumentNullException(nameof(loggerConfig));
            this._loggerConfigs = loggerConfig((object)this);
            _localizer = localizer;
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
        /// <exception cref="MyValidationException"></exception>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            LoggerSingleton.Debug(this._loggerConfigs, "MediatrValidator::Validating " + typeof(TRequest).FullName);
            List<ValidationFailure> list = _validators
                .Select<IValidator<TRequest>, ValidationResult>((Func<IValidator<TRequest>, ValidationResult>)
                    (v => v.Validate(request))).SelectMany<ValidationResult, ValidationFailure>(
                    (Func<ValidationResult, IEnumerable<ValidationFailure>>)
                    (result => (IEnumerable<ValidationFailure>)result.Errors))
                .Where<ValidationFailure>((Func<ValidationFailure, bool>)(error => error != null))
                .ToList<ValidationFailure>();

            if (((IEnumerable<ValidationFailure>)list).Any<ValidationFailure>())
            {
                LoggerSingleton.Warn(this._loggerConfigs, string.Format(
                    "MediatrValidator::Validation errors - {0} - Command: {1} - Errors: {2}",
                    (object)typeof(TRequest).FullName, (object)request, (object)string.Join("; ", list)));
                throw new MyValidationException(list, _localizer);
            }

            LoggerSingleton.Debug(this._loggerConfigs, "MediatrValidator::Validated " + typeof(TRequest).FullName);
            return await next();
        }
    }
}
