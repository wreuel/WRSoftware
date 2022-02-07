using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

/// <summary>
/// The Command Repository following the CQRS Pattern
/// Here is only possible to do Update and Insert of Data
/// </summary>
namespace WRSoftware.Utils.EntityFrameworkCore.Interfaces.CQRS
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="WRSoftware.Utils.EntityFrameworkCore.Interfaces.IRepository" />
    public interface ICommandRepository<TEntity> : IRepository where TEntity : class
    {


        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Hards the delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void HardDelete(long id);

        /// <summary>
        /// Hards the delete.
        /// </summary>
        /// <param name="filter">The filter.</param>
        void HardDelete(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Hards the delete.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void HardDelete(TEntity entity);

        /// <summary>
        /// Hards the delete asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task HardDeleteAsync(long id);

        /// <summary>
        /// Hards the delete asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task HardDeleteAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Hards the delete asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task HardDeleteAsync(TEntity entity);

        /// <summary>
        /// Hards the delete many asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task HardDeleteManyAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Hards the delete many asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task HardDeleteManyAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Hards the delete many.
        /// </summary>
        /// <param name="filter">The filter.</param>
        void HardDeleteMany(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Hards the delete many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void HardDeleteMany(IEnumerable<TEntity> entities);


    }
}