using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WRSoftware.Utils.EntityFrameworkCore.Interfaces.CQRS;

namespace WRSoftware.Utils.EntityFrameworkCore.Repositories.CQRS
{
    /// <summary>
    /// The Real Implementation of Generic Command Repository following the CQRS Pattern
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="WRSoftware.Utils.EntityFrameworkCore.Repositories.BaseRepository{TEntity}" />
    /// <seealso cref="WRSoftware.Utils.EntityFrameworkCore.Interfaces.CQRS.ICommandRepository{TEntity}" />
    /// <seealso cref="WRSoftware.Utils.EntityFrameworkCore.Interfaces.IRepository" />
    public abstract class GenericCommandRepository<TEntity> : BaseRepository<TEntity>, ICommandRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCommandRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected GenericCommandRepository(BaseDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Hards the delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void HardDelete(long id)
        {
            this.HardDelete(this._dbSet.Find((object)id));
        }

        /// <summary>
        /// Hards the delete.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public virtual void HardDelete(Expression<Func<TEntity, bool>> filter)
        {
            this.HardDelete(this._dbSet.Single<TEntity>(filter));
        }

        /// <summary>
        /// Hards the delete.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void HardDelete(TEntity entity)
        {
            if (this._context.Entry<TEntity>(entity).State.Equals((object)EntityState.Detached))
                this._dbSet.Attach(entity);
            this._dbSet.Remove(entity);
        }

        /// <summary>
        /// Hards the delete asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual async Task HardDeleteAsync(long id)
        {
            GenericCommandRepository<TEntity> commandRepository = this;
            TEntity entity = await commandRepository._dbSet.FindAsync((object)id);
            await commandRepository.HardDeleteAsync(entity);
        }

        /// <summary>
        /// Hards the delete asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public virtual async Task HardDeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            GenericCommandRepository<TEntity> commandRepository = this;
            TEntity entityToDelete =
                await commandRepository._dbSet.SingleAsync<TEntity>(filter, new CancellationToken());
            await commandRepository.HardDeleteAsync(entityToDelete);
        }

        /// <summary>
        /// Hards the delete asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual async Task HardDeleteAsync(TEntity entity)
        {
            await Task.Run((Action)(() =>
           {
               if (this._context.Entry<TEntity>(entity).State.Equals((object)EntityState.Detached))
               {
                   this._dbSet.Attach(entity);
               }

               this._dbSet.Remove(entity);
           }));
        }

        /// <summary>
        /// Hards the delete many.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public virtual void HardDeleteMany(Expression<Func<TEntity, bool>> filter)
        {
            this.HardDeleteMany((IEnumerable<TEntity>)this._dbSet.Where<TEntity>(filter).ToList());
        }

        /// <summary>
        /// Hards the delete many.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void HardDeleteMany(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                if (this._context.Entry<TEntity>(entity).State.Equals((object)EntityState.Detached))
                    this._dbSet.Attach(entity);
                this._dbSet.Remove(entity);
            }
        }

        /// <summary>
        /// Hards the delete many asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public virtual async Task HardDeleteManyAsync(Expression<Func<TEntity, bool>> filter)
        {
            GenericCommandRepository<TEntity> commandRepository = this;
            List<TEntity> list = commandRepository._dbSet.Where<TEntity>(filter).ToList<TEntity>();
            await commandRepository.HardDeleteManyAsync((IEnumerable<TEntity>)list);
        }

        /// <summary>
        /// Hards the delete many asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual async Task HardDeleteManyAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run((Action)(() =>
           {
               foreach (TEntity entity in entities)
               {
                   if (this._context.Entry<TEntity>(entity).State.Equals((object)EntityState.Detached))
                   {
                       this._dbSet.Attach(entity);
                   }

                   this._dbSet.Remove(entity);
               }
           }));
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual TEntity Insert(TEntity entity)
        {
            return this._dbSet.Add(entity).Entity;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            return (await this._dbSet.AddAsync(entity, new CancellationToken())).Entity;
        }

        public virtual void Update(TEntity entity)
        {
            this._dbSet.Attach(entity);
            this._context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            await Task.Run((Action)(() =>
           {
               this._dbSet.Attach(entity);
               this._context.Entry<TEntity>(entity).State = EntityState.Modified;
           }));
        }
    }
}