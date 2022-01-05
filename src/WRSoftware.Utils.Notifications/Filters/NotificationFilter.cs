using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WRSoftware.Utils.Notifications.Context;

namespace WRSoftware.Utils.Notifications.Filters
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IAsyncResultFilter" />
    public class NotificationFilter : IAsyncResultFilter
    {
        /// <summary>
        /// The notification context
        /// </summary>
        private readonly NotificationContext _notificationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationFilter"/> class.
        /// </summary>
        /// <param name="notificationContext">The notification context.</param>
        public NotificationFilter(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        /// <summary>
        /// Called asynchronously before the action result.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ResultExecutingContext" />.</param>
        /// <param name="next">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ResultExecutionDelegate" />. Invoked to execute the next result filter or the result itself.</param>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_notificationContext.HasErrors)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/json";

                var dictionary = _notificationContext.Errors.GroupBy(n => n.Key)
                    .ToDictionary(x => x.Key, x => new List<string>());

                foreach (var notification in _notificationContext.Errors)
                {
                    dictionary[notification.Key].Add(notification.Message);
                }

                await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(dictionary));

                return;
            }

            await next();
        }
    }
}
