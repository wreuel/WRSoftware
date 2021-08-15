using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using WRSoftware.Utils.Logging;

namespace WRSoftware.Utils.MVC.Filters
{
    /// <summary>
    /// Treat the Exception launched by the MVC Pages
    /// When this exepction is not treated by the Page itself
    /// </summary>
    public class MvcExceptionMiddlewarePage
    {
        /// <summary>
        /// The logger configs
        /// </summary>
        protected readonly LoggerConfig _loggerConfigs;

        /// <summary>
        /// The request delegate
        /// </summary>
        private readonly RequestDelegate _requestDelegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="MvcExceptionMiddlewarePage"/> class.
        /// </summary>
        /// <param name="requestDelegate">The request delegate.</param>
        /// <param name="loggerConfigs">The logger configs.</param>
        /// <exception cref="ArgumentNullException">
        /// loggerConfigs
        /// or
        /// requestDelegate
        /// </exception>
        public MvcExceptionMiddlewarePage(RequestDelegate requestDelegate, Func<object, LoggerConfig> loggerConfigs)
        {
            _loggerConfigs = loggerConfigs != null
                ? loggerConfigs(this)
                : throw new ArgumentNullException(nameof(loggerConfigs));
            _requestDelegate = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate));
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exception asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="errorCode">The error code.</param>
        private void HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var message = $"Running Request: {context.Request.Path.Value} - TraceIdentifier: {context.TraceIdentifier}";
            LoggerSingleton.Error(_loggerConfigs, exception, message);


            ///var dot = new ResponseDto()
            ///{
            ///    Messages = new List<string>
            ///    {
            ///        exception.Message,
            ///        exception.StackTrace,
            ///        exception.InnerException?.Message,
            ///        exception.InnerException?.StackTrace
            ///    },
            ///    StatusCode = errorCode,
            ///    Succeeded = false
            ///};


            ///var str = JsonConvert.SerializeObject(dot);

            ///var query = StringHelper.JsonToQuery(str);
            /// var uri = new Uri(context.Request.Path.Value);

            ///var url = new Uri(uri, "~/error" + query);
            ///var c = "https:///localhost:44376" + $"/error?succeeded=true&statusCode{dot.StatusCode}";

            ///var url = context.Request.Path.Value + "/error" + query;

            ///context.Response.ContentType = "application/json";
            ///context.Response.StatusCode = errorCode;


            throw exception;

            ///context.Response.Redirect(c.ToString());
        }
    }
}
