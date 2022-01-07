using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WRSoftware.Utils.Common.Models;
using WRSoftware.Utils.EntityFrameworkCore.Extensions;
using WRSoftware.Utils.EntityFrameworkCore.Interfaces;
using WRSoftware.Utils.EntityFrameworkCore.Repositories;

namespace WRSoftware.Utils.EntityFramework.Repositories
{
    /// <summary>
    /// The Repository as only one for all the commands and queries
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="WRSoftware.Utils.EntityFrameworkCore.Repositories.BaseRepository{TEntity}" />
    /// <seealso cref="WRSoftware.Utils.EntityFrameworkCore.Interfaces.IGenericRepository{TEntity}" />
    /// <seealso cref="WRSoftware.Utils.EntityFrameworkCore.Interfaces.IRepository" />
    public abstract class GenericRepository<TEntity> : BaseRepository<TEntity>, IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected GenericRepository(BaseDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Counts the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int Count(Expression<Func<TEntity, bool>> filter)
        {
            return this.Count(filter, (string)null);
        }

        /// <summary>
        /// Counts the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {
            return this._dbSet.WithIncludeProperties<TEntity>(includeProperties)
                .WithFilter<TEntity>(filter).Count<TEntity>();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await this._dbSet.CountAsync(filter);
        }

        /// <summary>
        /// Counts the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {
            return await this._dbSet.WithIncludeProperties<TEntity>(includeProperties)
                .WithFilter<TEntity>(filter)
                .CountAsync<TEntity>(new CancellationToken());
        }

        /// <summary>
        /// Existses the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public bool Exists(Expression<Func<TEntity, bool>> filter)
        {
            return this.Exists(filter, (string)null);
        }

        /// <summary>
        /// Existses the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public bool Exists(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {
            try
            {
                return this.Count(filter, includeProperties) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Existses the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await this.ExistsAsync(filter, (string)null);
        }

        /// <summary>
        /// Existses the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {
            try
            {
                return await this.CountAsync(filter, includeProperties) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Existses the single.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public bool ExistsSingle(Expression<Func<TEntity, bool>> filter)
        {
            return this.ExistsSingle(filter, (string)null);
        }

        /// <summary>
        /// Existses the single.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public bool ExistsSingle(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {
            try
            {
                return this.Count(filter, includeProperties) == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Existses the single asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<bool> ExistsSingleAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await this.ExistsSingleAsync(filter, (string)null);
        }

        /// <summary>
        /// Existses the single asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public async Task<bool> ExistsSingleAsync(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {
            try
            {
                return await this.CountAsync(filter, includeProperties) == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual TEntity GetItem(long id)
        {
            return this._dbSet.Find((object)id);
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual TEntity GetItem(Expression<Func<TEntity, bool>> filter)
        {
            return this.GetItem(filter, (string)null);
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public TEntity GetItem(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {
            return this._dbSet.WithIncludeProperties<TEntity>(includeProperties)
                .SingleOrDefault<TEntity>(filter);
        }

        /// <summary>
        /// Gets the item asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetItemAsync(long id)
        {
            return await this._dbSet.FindAsync((object)id);
        }

        /// <summary>
        /// Gets the item asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetItemAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await this.GetItemAsync(filter, (string)null);
        }

        /// <summary>
        /// Gets the item asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetItemAsync(Expression<Func<TEntity, bool>> filter,
            string includeProperties)
        {
            return await this._dbSet.WithIncludeProperties<TEntity>(includeProperties)
                .SingleOrDefaultAsync<TEntity>(filter, new CancellationToken());
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns></returns>
        public virtual List<TEntity> GetList()
        {
            return this.GetListAux<TEntity>((Expression<Func<TEntity, bool>>)null,
                (Expression<Func<TEntity, TEntity>>)null,
                new ListSortDirection?(), new int?(), new int?(), (string)null);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> filter)
        {
            return this.GetListAux<TEntity>(filter, (Expression<Func<TEntity, TEntity>>)null, new ListSortDirection?(),
                new int?(), new int?(), (string)null);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public List<TEntity> GetList(string includeProperties)
        {
            return this.GetListAux<TEntity>((Expression<Func<TEntity, bool>>)(p => true),
                (Expression<Func<TEntity, TEntity>>)null,
                new ListSortDirection?(), new int?(), new int?(), includeProperties);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {
            return this.GetListAux<TEntity>(filter, (Expression<Func<TEntity, TEntity>>)null, new ListSortDirection?(),
                new int?(), new int?(), includeProperties);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="sortingDirection">The sorting direction.</param>
        /// <returns></returns>
        public virtual List<TEntity> GetList<TKey>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> orderBy, ListSortDirection sortingDirection)
        {
            return this.GetListAux<TKey>(filter, orderBy, new ListSortDirection?(sortingDirection), new int?(),
                new int?(), (string)null);
        }

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetListAsync()
        {
            return await this.GetListAuxAsync<TEntity>((Expression<Func<TEntity, bool>>)null,
                (Expression<Func<TEntity, TEntity>>)null, new ListSortDirection?(), new int?(), new int?(),
                (string)null);
        }

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await this.GetListAuxAsync<TEntity>(filter, (Expression<Func<TEntity, TEntity>>)null,
                new ListSortDirection?(), new int?(), new int?(), (string)null);
        }

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetListAsync(string includeProperties)
        {
            return await this.GetListAuxAsync<TEntity>((Expression<Func<TEntity, bool>>)(p => true),
                (Expression<Func<TEntity, TEntity>>)null,
                new ListSortDirection?(), new int?(), new int?(), includeProperties);
        }

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter,
            string includeProperties)
        {
            return await this.GetListAuxAsync<TEntity>(filter, (Expression<Func<TEntity, TEntity>>)null,
                new ListSortDirection?(), new int?(), new int?(), includeProperties);
        }

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="sortingDirection">The sorting direction.</param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetListAsync<TKey>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> orderBy,
            ListSortDirection sortingDirection)
        {
            return await this.GetListAuxAsync<TKey>(filter, orderBy, new ListSortDirection?(sortingDirection),
                new int?(), new int?(), (string)null);
        }

        /// <summary>
        /// Gets the paginated list.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="sortingDirection">The sorting direction.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public virtual Paginated<TEntity> GetPaginatedList<TKey>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> orderBy,
            ListSortDirection sortingDirection, int page, int pageSize)
        {
            return new Paginated<TEntity>(this.Count(filter), pageSize, page,
                (IEnumerable<TEntity>)this.GetListAux<TKey>(filter, orderBy,
                    new ListSortDirection?(sortingDirection), new int?(page), new int?(pageSize), (string)null));
        }

        /// <summary>
        /// Gets the paginated list.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="sortingDirection">The sorting direction.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual Paginated<TEntity> GetPaginatedList<TKey>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> orderBy,
            ListSortDirection sortingDirection, int page, int pageSize, string includeProperties)
        {
            return new Paginated<TEntity>(this.Count(filter), pageSize, page,
                (IEnumerable<TEntity>)this.GetListAux<TKey>(filter, orderBy,
                    new ListSortDirection?(sortingDirection), new int?(page), new int?(pageSize), includeProperties));
        }

        /// <summary>
        /// Gets the paginated list asynchronous.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="sortingDirection">The sorting direction.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public virtual async Task<Paginated<TEntity>> GetPaginatedListAsync<TKey>(
            Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy,
            ListSortDirection sortingDirection, int page, int pageSize)
        {
            int count = await this.CountAsync(filter, (string)null);

            return new Paginated<TEntity>(count, pageSize, page,
                (IEnumerable<TEntity>)await this.GetListAuxAsync<TKey>(filter, orderBy,
                    new ListSortDirection?(sortingDirection), new int?(page), new int?(pageSize), (string)null));
        }

        /// <summary>
        /// Gets the paginated list asynchronous.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="sortingDirection">The sorting direction.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<Paginated<TEntity>> GetPaginatedListAsync<TKey>(
            Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy,
            ListSortDirection sortingDirection, int page, int pageSize, string includeProperties)
        {
            int count = await this.CountAsync(filter, (string)null);
            return new Paginated<TEntity>(count, pageSize, page,
                (IEnumerable<TEntity>)await this.GetListAuxAsync<TKey>(filter, orderBy,
                    new ListSortDirection?(sortingDirection), new int?(page), new int?(pageSize), includeProperties));
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
            GenericRepository<TEntity> commandRepository = this;
            TEntity entity = await commandRepository._dbSet.FindAsync((object)id);
            await commandRepository.HardDeleteAsync(entity);
        }

        /// <summary>
        /// Hards the delete asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public virtual async Task HardDeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            GenericRepository<TEntity> commandRepository = this;
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
            GenericRepository<TEntity> commandRepository = this;
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

        /// <summary>
        /// Gets the list aux.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="sortingDirection">The sorting direction.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        protected List<TEntity> GetListAux<TKey>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> orderBy,
            ListSortDirection? sortingDirection, int? page, int? pageSize, string includeProperties)
        {
            return this._dbSet.WithFilter<TEntity>(filter)
                .WithIncludeProperties<TEntity>(includeProperties)
                .WithOrderBy<TEntity, TKey>(orderBy, sortingDirection).WithPagination<TEntity>(page, pageSize)
                .ToList<TEntity>();
        }

        /// <summary>
        /// Gets the list aux asynchronous.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="sortingDirection">The sorting direction.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        protected async Task<List<TEntity>> GetListAuxAsync<TKey>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> orderBy,
            ListSortDirection? sortingDirection, int? page, int? pageSize, string includeProperties)
        {
            return await this._dbSet.WithFilter<TEntity>(filter)
                .WithIncludeProperties<TEntity>(includeProperties)
                .WithOrderBy<TEntity, TKey>(orderBy, sortingDirection)
                .WithPagination<TEntity>(page, pageSize).ToListAsync<TEntity>(new CancellationToken());
        }
    }
}