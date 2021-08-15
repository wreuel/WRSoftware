using System;
using System.ComponentModel;
using System.Threading.Tasks;
using MediatR;

namespace WRSoftware.Utils.MediatRHangfire
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class MediatorHangfireBridge
    {
        /// <summary>The mediator</summary>
        private readonly IMediator _mediator;

        /// <summary>Initializes a new instance of the <see cref="MediatorHangfireBridge" /> class.</summary>
        /// <param name="mediator">The mediator.</param>
        /// <exception cref="System.ArgumentNullException">mediator</exception>
        public MediatorHangfireBridge(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>Sends the specified command.</summary>
        /// <param name="command">The command.</param>
        public async Task Send(IRequest command)
        {
            await _mediator.Send(command);
        }

        /// <summary>Sends the specified command.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command.</param>
        public async Task Send<T>(IRequest<T> command)
        {
            await _mediator.Send(command);
        }

        /// <summary>
        /// Sends the specified job name.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="command">The command.</param>
        [DisplayName("{0}")]
        public async Task Send(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }
    }
}
