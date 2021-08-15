using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WRSoftware.Utils.Logging;

namespace WRSoftware.Utils.REST.Filters
{
    /// <summary>
    /// Filter that will check the performance
    /// on a API Request, if takes longer that 500 ms
    /// It will register a Warning on the Log
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IAsyncActionFilter" />
    public class ApiPerformanceFilter : IAsyncActionFilter
    {
        /// <summary>
        /// The timer
        /// </summary>
        private readonly Stopwatch _timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiPerformanceFilter"/> class.
        /// </summary>
        public ApiPerformanceFilter()
        {
            _timer = new Stopwatch();
        }

        /// <summary>
        /// Called asynchronously before the action, after model binding is complete.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
        /// <param name="next">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutionDelegate" />. Invoked to execute the next action filter or the action itself.</param>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            LoggerConfig _loggerConfigs = context.HttpContext.RequestServices.GetService<Func<object, LoggerConfig>>()(this);

            _timer.Start();

            await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = context.Controller.ToString();

                LoggerSingleton.Warn(_loggerConfigs,
                    $"Long Running Request: {requestName} - TraceIdentifier: {context.HttpContext.TraceIdentifier} - {elapsedMilliseconds} milliseconds");
            }
        }
    }
}