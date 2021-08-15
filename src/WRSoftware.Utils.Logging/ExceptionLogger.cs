using Castle.DynamicProxy;
using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WRSoftware.Utils.Logging
{
    /// <summary>
    /// Interceptor of Exception to write on the Logs
    /// </summary>
    /// <seealso cref="Castle.DynamicProxy.IInterceptor" />
    public class ExceptionLogger : IInterceptor
    {
        /// <summary>
        /// The logger configs resolver
        /// </summary>
        private readonly Func<object, LoggerConfig> _loggerConfigsResolver;

        /// <summary>
        /// The warn level enabled
        /// </summary>
        private readonly bool warnLevelEnabled = ((IEnumerable<LoggingRule>)LogManager.Configuration.LoggingRules)
            .Single<LoggingRule>(t => "*".Equals(t.LoggerNamePattern))
            .IsLoggingEnabledForLevel(LogLevel.Warn);

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionLogger"/> class.
        /// </summary>
        /// <param name="loggerConfig">The logger configuration.</param>
        /// <exception cref="System.ArgumentNullException">loggerConfig</exception>
        public ExceptionLogger(Func<object, LoggerConfig> loggerConfig)
        {
            Func<object, LoggerConfig> func = loggerConfig;
            this._loggerConfigsResolver = func ?? throw new ArgumentNullException(nameof(loggerConfig));
        }

        /// <summary>
        /// Intercepts the specified invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            LoggerConfig configs = this._loggerConfigsResolver((object)this);
            if (this.warnLevelEnabled)
            {
                try
                {
                    invocation.Proceed();
                }
                catch (Exception ex)
                {
                    string methodName = invocation.TargetType.ToString() + "." + invocation.Method.Name;
                    LoggerSingleton.StructuredLog(configs, ex,
                        "ExceptionLogger::" + methodName + " - caught exception ", (string)null,
                        LogLevel.Warn, new DateTime?(DateTime.Now), methodName, (string)null, (string)null,
                        new long?());
                    throw;
                }
            }
            else
                invocation.Proceed();
        }
    }
}
