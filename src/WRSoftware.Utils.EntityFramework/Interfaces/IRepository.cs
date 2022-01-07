using System.Threading;
using System.Threading.Tasks;

namespace WRSoftware.Utils.EntityFrameworkCore.Interfaces
{
    /// <summary>
    /// The Repository itself
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Saves the entities asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Saves the entities.
        /// </summary>
        /// <returns></returns>
        bool SaveEntities();

    }
}