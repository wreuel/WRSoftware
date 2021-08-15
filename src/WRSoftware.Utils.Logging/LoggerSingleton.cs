using NLog;
using System;
using System.Collections.Generic;

namespace WRSoftware.Utils.Logging
{
    /// <summary>
    /// Logger Singleton, who will find the Log instance 
    /// and write on the file
    /// </summary>
    public sealed class LoggerSingleton
    {
        /// <summary>
        /// The datetime2 mask
        /// </summary>
        private const string datetime2Mask = "yyyy-MM-dd HH:mm:ss.ffffff";

        /// <summary>
        /// The synchronize root
        /// </summary>
        private static readonly object _syncRoot = new object();

        /// <summary>
        /// The instance
        /// </summary>
        private static volatile LoggerSingleton _instance;

        /// <summary>
        /// The loggers
        /// </summary>
        private readonly Dictionary<string, ILogger> _loggers;

        /// <summary>
        /// Prevents a default instance of the <see cref="LoggerSingleton"/> class from being created.
        /// </summary>
        private LoggerSingleton()
        {
            this._loggers = new Dictionary<string, ILogger>();
        }

        /// <summary>
        /// Gets the loggers.
        /// </summary>
        /// <value>
        /// The loggers.
        /// </value>
        private static Dictionary<string, ILogger> Loggers
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new LoggerSingleton();
                    }
                }

                return _instance._loggers;
            }
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="loggerName">Name of the logger.</param>
        /// <returns></returns>
        private static ILogger GetLogger(string loggerName)
        {
            if (!Loggers.ContainsKey(loggerName))
            {
                lock (_syncRoot)
                {
                    if (!Loggers.ContainsKey(loggerName))
                        Loggers.Add(loggerName, (ILogger)LogManager.GetLogger(loggerName));
                }
            }

            return Loggers[loggerName];
        }

        /// <summary>
        /// Sets the log information properties.
        /// </summary>
        /// <param name="logInfo">The log information.</param>
        /// <param name="configs">The configs.</param>
        /// <param name="additionalData">The additional data.</param>
        private static void SetLogInfoProperties(LogEventInfo logInfo, LoggerConfig configs, string additionalData)
        {
            logInfo.Properties.Add(nameof(additionalData), additionalData);
            logInfo.Properties.Add("service", configs.ServiceContext.Service);
            logInfo.Properties.Add("operation", configs.ServiceContext.Operation);
            logInfo.Properties.Add("callTimestamp", DateTime.Now.ToString(datetime2Mask));
        }

        /// <summary>
        /// Sets the log information properties.
        /// </summary>
        /// <param name="logInfo">The log information.</param>
        /// <param name="configs">The configs.</param>
        /// <param name="additionalData">The additional data.</param>
        /// <param name="callTimestamp">The call timestamp.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        /// <param name="responseTime">The response time.</param>
        private static void SetLogInfoProperties(LogEventInfo logInfo, LoggerConfig configs, string additionalData,
            DateTime? callTimestamp,
            string methodName, string request, string response, long? responseTime)
        {
            logInfo.Properties.Add(nameof(additionalData), additionalData);
            logInfo.Properties.Add("service", configs.ServiceContext.Service);
            logInfo.Properties.Add("operation", configs.ServiceContext.Operation);
            logInfo.Properties.Add(nameof(callTimestamp), callTimestamp?.ToString(datetime2Mask));
            logInfo.Properties.Add(nameof(methodName), methodName);
            logInfo.Properties.Add(nameof(request), request);
            logInfo.Properties.Add(nameof(response), response);
            logInfo.Properties.Add(nameof(responseTime), responseTime);
        }


        /// <summary>
        /// Logs the specified configs.
        /// </summary>
        /// <param name="configs">The configs.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="logLevel">The log level.</param>
        private static void Log(LoggerConfig configs, Exception exception, LogLevel logLevel)
        {
            ILogger logger = GetLogger(configs.LoggerName);
            LogEventInfo logInfo = new LogEventInfo(logLevel, ((ILoggerBase)logger).Name, (IFormatProvider)null,
                exception?.Message, (object[])null, exception);
            SetLogInfoProperties(logInfo, configs, (string)null);
            ((ILoggerBase)logger).Log(typeof(LoggerSingleton), logInfo);
        }

        /// <summary>
        /// Logs the specified configs.
        /// </summary>
        /// <param name="configs">The configs.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="additionalData">The additional data.</param>
        /// <param name="logLevel">The log level.</param>
        private static void Log(LoggerConfig configs, Exception exception, string message, string additionalData,
            LogLevel logLevel)
        {
            ILogger logger = GetLogger(configs.LoggerName);
            LogEventInfo logInfo = new LogEventInfo(logLevel, ((ILoggerBase)logger).Name, (IFormatProvider)null,
                message, (object[])null, exception);
            SetLogInfoProperties(logInfo, configs, additionalData);
            ((ILoggerBase)logger).Log(typeof(LoggerSingleton), logInfo);
        }

        /// <summary>
        /// Logs the specified configs.
        /// </summary>
        /// <param name="configs">The configs.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="logLevel">The log level.</param>
        private static void Log(LoggerConfig configs, Exception exception, string message, LogLevel logLevel)
        {
            ILogger logger = GetLogger(configs.LoggerName);
            LogEventInfo logInfo = new LogEventInfo(logLevel, ((ILoggerBase)logger).Name, (IFormatProvider)null,
                message, (object[])null, exception);
            SetLogInfoProperties(logInfo, configs, (string)null);
            ((ILoggerBase)logger).Log(typeof(LoggerSingleton), logInfo);
        }

        /// <summary>
        /// Logs the specified configs.
        /// </summary>
        /// <param name="configs">The configs.</param>
        /// <param name="message">The message.</param>
        /// <param name="logLevel">The log level.</param>
        private static void Log(LoggerConfig configs, string message, LogLevel logLevel)
        {
            Log(configs, message, (string)null, logLevel);
        }

        /// <summary>
        /// Logs the specified configs.
        /// </summary>
        /// <param name="configs">The configs.</param>
        /// <param name="message">The message.</param>
        /// <param name="additionalData">The additional data.</param>
        /// <param name="logLevel">The log level.</param>
        private static void Log(LoggerConfig configs, string message, string additionalData, LogLevel logLevel)
        {
            ILogger logger = GetLogger(configs.LoggerName);
            LogEventInfo logInfo = new LogEventInfo(logLevel, ((ILoggerBase)logger).Name, (IFormatProvider)null,
                message, (object[])null);
            SetLogInfoProperties(logInfo, configs, additionalData);
            ((ILoggerBase)logger).Log(typeof(LoggerSingleton), logInfo);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Trace level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        public static void StructuredLog(LoggerConfig configs, Exception exception, string message,
            string additionalData, LogLevel logLevel, DateTime? callTimestamp,
            string methodName, string request, string response, long? responseTime)
        {
            ILogger logger = GetLogger(configs.LoggerName);
            LogEventInfo logInfo = new LogEventInfo(logLevel, ((ILoggerBase)logger).Name, (IFormatProvider)null,
                message, (object[])null, exception);
            SetLogInfoProperties(logInfo, configs, additionalData, callTimestamp, methodName, request, response,
                responseTime);
            ((ILoggerBase)logger).Log(typeof(LoggerSingleton), logInfo);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Trace level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        public static void Trace(LoggerConfig configs, Exception exception)
        {
            Log(configs, exception, LogLevel.Trace);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Trace level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        public static void Trace(LoggerConfig configs, Exception exception, string message)
        {
            Log(configs, exception, message, LogLevel.Trace);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Trace level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="additionalData">Aditional data to be logged (ex: payloads)</param>
        public static void Trace(LoggerConfig configs, Exception exception, string message, string additionalData)
        {
            Log(configs, exception, message, additionalData, LogLevel.Trace);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Trace level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="message">A string to be written.</param>
        public static void Trace(LoggerConfig configs, string message)
        {
            Log(configs, message, LogLevel.Trace);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Trace level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="additionalData">Aditional data to be logged (ex: payloads)</param>
        public static void Trace(LoggerConfig configs, string message, string additionalData)
        {
            Log(configs, message, additionalData, LogLevel.Trace);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Debug level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        public static void Debug(LoggerConfig configs, Exception exception)
        {
            Log(configs, exception, LogLevel.Debug);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Debug level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        public static void Debug(LoggerConfig configs, Exception exception, string message)
        {
            Log(configs, exception, message, LogLevel.Debug);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Debug level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="additionalData">Aditional data to be logged (ex: payloads)</param>
        public static void Debug(LoggerConfig configs, Exception exception, string message, string additionalData)
        {
            Log(configs, exception, message, additionalData, LogLevel.Debug);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Debug level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="message">A string to be written.</param>
        public static void Debug(LoggerConfig configs, string message)
        {
            Log(configs, message, LogLevel.Debug);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Debug level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="additionalData">Aditional data to be logged (ex: payloads)</param>
        public static void Debug(LoggerConfig configs, string message, string additionalData)
        {
            Log(configs, message, additionalData, LogLevel.Debug);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Info level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        public static void Info(LoggerConfig configs, Exception exception)
        {
            Log(configs, exception, LogLevel.Info);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Info level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        public static void Info(LoggerConfig configs, Exception exception, string message)
        {
            Log(configs, exception, message, LogLevel.Info);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Info level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="additionalData">Aditional data to be logged (ex: payloads)</param>
        public static void Info(LoggerConfig configs, Exception exception, string message, string additionalData)
        {
            Log(configs, exception, message, additionalData, LogLevel.Info);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Info level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="message">A string to be written.</param>
        public static void Info(LoggerConfig configs, string message)
        {
            Log(configs, message, LogLevel.Info);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Info level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="additionalData">Aditional data to be logged (ex: payloads)</param>
        public static void Info(LoggerConfig configs, string message, string additionalData)
        {
            Log(configs, message, additionalData, LogLevel.Info);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Warn level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        public static void Warn(LoggerConfig configs, Exception exception)
        {
            Log(configs, exception, LogLevel.Warn);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Warn level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        public static void Warn(LoggerConfig configs, Exception exception, string message)
        {
            Log(configs, exception, message, LogLevel.Warn);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Warn level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="additionalData">Aditional data to be logged (ex: payloads)</param>
        public static void Warn(LoggerConfig configs, Exception exception, string message, string additionalData)
        {
            Log(configs, exception, message, additionalData, LogLevel.Warn);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Warn level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="message">A string to be written.</param>
        public static void Warn(LoggerConfig configs, string message)
        {
            Log(configs, message, LogLevel.Warn);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Warn level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="additionalData">Aditional data to be logged (ex: payloads)</param>
        public static void Warn(LoggerConfig configs, string message, string additionalData)
        {
            Log(configs, message, additionalData, LogLevel.Warn);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Error level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        public static void Error(LoggerConfig configs, Exception exception)
        {
            Log(configs, exception, LogLevel.Error);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Error level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        public static void Error(LoggerConfig configs, Exception exception, string message)
        {
            Log(configs, exception, message, LogLevel.Error);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Error level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="additionalData">Aditional data to be logged (ex: payloads)</param>
        public static void Error(LoggerConfig configs, Exception exception, string message, string additionalData)
        {
            Log(configs, exception, message, additionalData, LogLevel.Error);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Error level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="message">A string to be written.</param>
        public static void Error(LoggerConfig configs, string message)
        {
            Log(configs, message, LogLevel.Error);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Error level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="additionalData">Aditional data to be logged (ex: payloads)</param>
        public static void Error(LoggerConfig configs, string message, string additionalData)
        {
            Log(configs, message, additionalData, LogLevel.Error);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Fatal level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        public static void Fatal(LoggerConfig configs, Exception exception)
        {
            Log(configs, exception, LogLevel.Fatal);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Fatal level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        public static void Fatal(LoggerConfig configs, Exception exception, string message)
        {
            Log(configs, exception, message, LogLevel.Fatal);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Fatal level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="additionalData">Aditional data to be logged (ex: payloads)</param>
        public static void Fatal(LoggerConfig configs, Exception exception, string message, string additionalData)
        {
            Log(configs, exception, message, additionalData, LogLevel.Fatal);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Fatal level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="message">A string to be written.</param>
        public static void Fatal(LoggerConfig configs, string message)
        {
            Log(configs, message, LogLevel.Fatal);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the Fatal level.
        /// </summary>
        /// <param name="configs">The logger configurations name to use for this entry</param>
        /// <param name="message">A string to be written.</param>
        /// <param name="additionalData">Aditional data to be logged (ex: payloads)</param>
        public static void Fatal(LoggerConfig configs, string message, string additionalData)
        {
            Log(configs, message, additionalData, LogLevel.Fatal);
        }
    }
}
