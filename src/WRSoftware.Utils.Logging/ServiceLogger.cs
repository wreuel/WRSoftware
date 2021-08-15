using Castle.DynamicProxy;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WRSoftware.Utils.Common.Interfaces;

namespace WRSoftware.Utils.Logging
{
    /// <summary>
    /// Interceptor of Service Logger
    /// </summary>
    /// <seealso cref="Castle.DynamicProxy.IInterceptor" />
    public class ServiceLogger : IInterceptor
    {
        /// <summary>
        /// The logger configs resolver
        /// </summary>
        private readonly Func<object, LoggerConfig> _loggerConfigsResolver;

        /// <summary>
        /// The service context
        /// </summary>
        private readonly IServiceContext _serviceContext;

        /// <summary>
        /// The information level enabled
        /// </summary>
        private readonly bool infoLevelEnabled = ((IEnumerable<LoggingRule>)LogManager.Configuration.LoggingRules)
            .Single<LoggingRule>((Func<LoggingRule, bool>)(t => "*".Equals(t.LoggerNamePattern)))
            .IsLoggingEnabledForLevel(LogLevel.Info);

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLogger" /> class.
        /// </summary>
        /// <param name="serviceContext">The service context.</param>
        /// <param name="loggerConfig">The logger configuration.</param>
        /// <exception cref="System.ArgumentNullException">serviceContext
        /// or
        /// loggerConfig</exception>
        public ServiceLogger(IServiceContext serviceContext, Func<object, LoggerConfig> loggerConfig)
        {
            this._serviceContext = serviceContext ?? throw new ArgumentNullException(nameof(serviceContext));

            Func<object, LoggerConfig> func = loggerConfig;
            this._loggerConfigsResolver = func ?? throw new ArgumentNullException(nameof(loggerConfig));
        }

        /// <summary>
        /// Intercepts a call to a service and logs it
        /// </summary>
        /// <param name="invocation">The invocation to be intercepted</param>
        public void Intercept(IInvocation invocation)
        {
            this._serviceContext.Service = invocation.TargetType.ToString();
            this._serviceContext.Operation = invocation.Method.Name;
            LoggerConfig configs = this._loggerConfigsResolver((object)this);
            if (this.infoLevelEnabled)
            {
                string methodName = invocation.TargetType.ToString() + "." + invocation.Method.Name;

                string request = string.Join(",", ((IEnumerable<object>)invocation.Arguments)
                    .Select<object, string>((Func<object, string>)(x => JsonConvert.SerializeObject(x)))
                    .ToArray<string>());

                LoggerSingleton.StructuredLog(configs, (Exception)null,
                    "ServiceLogger::" + methodName + " - Operation Request",
                    (string)null, LogLevel.Info, new DateTime?(DateTime.Now), methodName, request,
                    (string)null, new long?());

                Stopwatch stopwatch = Stopwatch.StartNew();

                invocation.Proceed();
                stopwatch.Stop();
                long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                LoggerSingleton.StructuredLog(configs, (Exception)null, string.Format(
                        "{0}::{1} - Operation Response in {2}ms",
                        (object)nameof(ServiceLogger), (object)methodName, (object)elapsedMilliseconds),
                    (string)null, LogLevel.Info, new DateTime?(DateTime.Now),
                    methodName, (string)null, JsonConvert.SerializeObject(invocation.ReturnValue),
                    new long?(elapsedMilliseconds));
            }
            else
                invocation.Proceed();
        }
    }
}
