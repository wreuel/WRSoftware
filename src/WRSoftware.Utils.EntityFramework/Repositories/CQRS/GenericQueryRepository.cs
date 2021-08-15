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
using WRSoftware.Utils.EntityFrameworkCore.Interfaces.CQRS;

namespace WRSoftware.Utils.EntityFrameworkCore.Repositories.CQRS
{
    /// <summary>
    /// The Real Implementation of Generic Query Repository following the CQRS Pattern
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="WRSoftware.Utils.EntityFrameworkCore.Repositories.BaseRepository{TEntity}" />
    /// <seealso cref="WRSoftware.Utils.EntityFrameworkCore.Interfaces.CQRS.IQueryRepository{TEntity}" />
    /// <seealso cref="WRSoftware.Utils.EntityFrameworkCore.Interfaces.IRepository" />
    public abstract class GenericQueryRepository<TEntity> : BaseRepository<TEntity>, IQueryRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericQueryRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected GenericQueryRepository(BaseDbContext context) : base(context)
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

        /// <summary>
        /// Counts the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
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