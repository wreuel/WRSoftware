using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WRSoftware.Utils.Common.DTO;
using WRSoftware.Utils.Logging;

namespace WRSoftware.Utils.WebBase.Middleware
{
    /// <summary>
    /// Middleware responsible for treat the 
    /// Exception not treated on the code
    /// </summary>
    public class ExceptionMiddleware
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
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="requestDelegate">The request delegate.</param>
        /// <param name="loggerConfigs">The logger configs.</param>
        /// <exception cref="ArgumentNullException">loggerConfigs</exception>
        public ExceptionMiddleware(RequestDelegate requestDelegate, Func<object, LoggerConfig> loggerConfigs)
        {
            _loggerConfigs = loggerConfigs != null
                ? loggerConfigs(this)
                : throw new ArgumentNullException(nameof(loggerConfigs));
            _requestDelegate = requestDelegate;
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
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exception asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="errorCode">The error code.</param>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception,
            int errorCode = (int)HttpStatusCode.InternalServerError)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorCode;
            LoggerSingleton.Error(_loggerConfigs, exception);
            await context.Response.WriteAsync(
                JsonConvert.SerializeObject(new ResponseDto
                {
                    Messages = new List<string> { "Erro Genérico" },
                    Succeeded = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                })
            );
        }
    }
}
