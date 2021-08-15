using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WRSoftware.Utils.Logging;

namespace WRSoftware.Utils.MVC.Filters
{
    /// <summary>
    /// Filter that will check the performance 
    /// Of a request on MVC Pages
    /// If the request takes more than 500ms it will Log a Warning
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IAsyncPageFilter" />
    public class MvcPerformanceFilter : IAsyncPageFilter
    {
        /// <summary>
        /// The timer
        /// </summary>
        private readonly Stopwatch _timer;

        /// <summary>
        /// The logger configs
        /// </summary>


        /// <summary>
        /// Initializes a new instance of the <see cref="MvcPerformanceFilter"/> class.
        /// </summary>
        public MvcPerformanceFilter()
        {
            _timer = new Stopwatch();
        }

        /// <summary>
        /// Called asynchronously after the handler method has been selected, but before model binding occurs.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.PageHandlerSelectedContext" />.</param>
        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Called asynchronously before the handler method is invoked, after model binding is complete.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext" />.</param>
        /// <param name="next">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutionDelegate" />. Invoked to execute the next page filter or the handler method itself.</param>
        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
            PageHandlerExecutionDelegate next)
        {
            LoggerConfig _loggerConfigs = context.HttpContext.RequestServices.GetService<Func<object, LoggerConfig>>()(this);

            _timer.Start();

            await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = context.HttpContext.Request.Path.ToString();

                LoggerSingleton.Warn(_loggerConfigs,
                    $"Long Running Request: {requestName} - TraceIdentifier: {context.HttpContext.TraceIdentifier} - {elapsedMilliseconds} milliseconds");
            }
        }
    }
}
