using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using WRSoftware.Utils.Notifications.Context;

namespace WRSoftware.Utils.Notifications.Filters
{
    public class NotificationFilterMvc : IAsyncResultFilter
    {
        private readonly NotificationContext _notificationContext;

        public NotificationFilterMvc(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_notificationContext.HasErrors)
            {
                _notificationContext.Errors.ToList().ForEach((x) =>
                {
                    context.ModelState.AddModelError(x.Key, x.Message);
                });
            }

            await next();
        }
    }
}
