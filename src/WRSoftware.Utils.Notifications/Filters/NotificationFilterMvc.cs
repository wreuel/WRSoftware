using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using WRSoftware.Utils.Notifications.Context;

namespace WRSoftware.Utils.Notifications.Filters
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IAsyncResultFilter" />
    public class NotificationFilterMvc : IAsyncResultFilter
    {
        /// <summary>
        /// The notification context
        /// </summary>
        private readonly NotificationContext _notificationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationFilterMvc"/> class.
        /// </summary>
        /// <param name="notificationContext">The notification context.</param>
        public NotificationFilterMvc(NotificationContext notificationContext)
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
                _notificationContext.Errors.ToList().ForEach((x) =>
                {
                    context.ModelState.AddModelError(x.Key, x.Message);
                });
            }

            await next();
        }
    }
}
