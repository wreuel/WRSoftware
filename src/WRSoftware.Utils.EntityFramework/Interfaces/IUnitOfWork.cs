using System;
using System.Threading;
using System.Threading.Tasks;

namespace WRSoftware.Utils.EntityFrameworkCore.Interfaces
{
    /// <summary>
    /// The Unit of Work
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>Saves the entities.</summary>
        /// <returns></returns>
        bool SaveEntities();

        /// <summary>Saves the entities asynchronous.</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}