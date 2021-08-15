using System;
using WRSoftware.Utils.Common.Interfaces;

namespace WRSoftware.Utils.Logging
{
    /// <summary>
    /// Set of Logger Configurations
    /// </summary>
    public class LoggerConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerConfig" /> class.
        /// </summary>
        /// <param name="loggerName">Name of the logger.</param>
        /// <param name="serviceContext">The service context.</param>
        /// <exception cref="System.ArgumentNullException">loggerName
        /// or
        /// serviceContext
        /// or
        /// callContext</exception>
        public LoggerConfig(string loggerName, IServiceContext serviceContext)
        {
            if (string.IsNullOrEmpty(loggerName))
                throw new ArgumentNullException(nameof(loggerName));
            LoggerName = loggerName;
            ServiceContext = serviceContext ?? throw new ArgumentNullException(nameof(serviceContext));
        }

        /// <summary>
        /// Gets the name of the logger.
        /// </summary>
        /// <value>
        /// The name of the logger.
        /// </value>
        public string LoggerName { get; }

        /// <summary>
        /// Gets the logger service.
        /// </summary>
        /// <value>
        /// The logger service.
        /// </value>
        public string LoggerService { get; private set; }

        /// <summary>
        /// Gets the service context.
        /// </summary>
        /// <value>
        /// The service context.
        /// </value>
        public IServiceContext ServiceContext { get; }
    }
}