using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WRSoftware.Utils.Notifications.Interfaces;

namespace WRSoftware.Utils.Notifications.Filters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly INotificationContext _notificationContext;

        public NotificationFilter(INotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

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
