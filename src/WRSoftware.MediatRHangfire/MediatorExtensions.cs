using Hangfire;
using MediatR;

namespace WRSoftware.MediatRHangfire
{
    /// <summary>
    /// 
    /// </summary>
    public static class MediatorExtensions
    {
        /// <summary>
        /// Enqueues the specified job name.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="request">The request.</param>
        public static void Enqueue(this IMediator mediator, string jobName, IRequest request)
        {
            var client = new BackgroundJobClient();
            client.Enqueue<MediatorHangfireBridge>(bridge => bridge.Send(jobName, request));
        }

        /// <summary>
        /// Enqueues the specified request.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="request">The request.</param>
        public static void Enqueue(this IMediator mediator, IRequest request)
        {
            var client = new BackgroundJobClient();
            client.Enqueue<MediatorHangfireBridge>(bridge => bridge.Send(request));
        }

        /// <summary>
        /// Enqueues the specified request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mediator">The mediator.</param>
        /// <param name="request">The request.</param>
        public static void Enqueue<T>(this IMediator mediator, IRequest<T> request)
        {
            var client = new BackgroundJobClient();
            client.Enqueue<MediatorHangfireBridge>(bridge => bridge.Send(request));
        }

        /// <summary>
        /// Adds the or update recurring job.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mediator">The mediator.</param>
        /// <param name="request">The request.</param>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="cronExpression">The cron expression.</param>
        public static void AddOrUpdateRecurringJob<T>(this IMediator mediator, IRequest<T> request, string jobName, string cronExpression)
        {
            var client = new RecurringJobManager();
            client.AddOrUpdate<MediatorHangfireBridge>(jobName, bridge => bridge.Send(request), cronExpression);

        }
    }
}
