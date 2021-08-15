using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WRSoftware.Utils.Common.Models;

namespace WRSoftware.Utils.EntityFrameworkCore.Interfaces.CQRS
{
    /// <summary>
    /// The Query Repository following the CQRS Pattern
    /// Here is only possible to make selects on data
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="WRSoftware.Utils.EntityFrameworkCore.Interfaces.IRepository" />
    public interface IQueryRepository<TEntity> : IRepository where TEntity : class
    {
        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        TEntity GetItem(long id);

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        TEntity GetItem(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        TEntity GetItem(Expression<Func<TEntity, bool>> filter, string includeProperties);

        /// <summary>
        /// Gets the item asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<TEntity> GetItemAsync(long id);

        /// <summary>
        /// Gets the item asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<TEntity> GetItemAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets the item asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<TEntity> GetItemAsync(Expression<Func<TEntity, bool>> filter, string includeProperties);

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetList();


        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        List<TEntity> GetList(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        List<TEntity> GetList(string includeProperties);

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        List<TEntity> GetList(Expression<Func<TEntity, bool>> filter, string includeProperties);

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="sortingDirection">The sorting direction.</param>
        /// <returns></returns>
        List<TEntity> GetList<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy,
            ListSortDirection sortingDirection);

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync();


        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter);


        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(string includeProperties);

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter, string includeProperties);

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="sortingDirection">The sorting direction.</param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync<TKey>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> orderBy, ListSortDirection sortingDirection);

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
        Paginated<TEntity> GetPaginatedList<TKey>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> orderBy,
            ListSortDirection sortingDirection, int page, int pageSize);

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
        Paginated<TEntity> GetPaginatedList<TKey>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> orderBy,
            ListSortDirection sortingDirection, int page, int pageSize, string includeProperties);

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
        Task<Paginated<TEntity>> GetPaginatedListAsync<TKey>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> orderBy,
            ListSortDirection sortingDirection, int page, int pageSize);

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
        Task<Paginated<TEntity>> GetPaginatedListAsync<TKey>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> orderBy,
            ListSortDirection sortingDirection, int page, int pageSize, string includeProperties);

        /// <summary>
        /// Existses the single.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        bool ExistsSingle(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Existses the single.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        bool ExistsSingle(Expression<Func<TEntity, bool>> filter, string includeProperties);

        /// <summary>
        /// Existses the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Existses the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> filter, string includeProperties);

        /// <summary>
        /// Existses the single asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<bool> ExistsSingleAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Existses the single asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<bool> ExistsSingleAsync(Expression<Func<TEntity, bool>> filter, string includeProperties);

        /// <summary>
        /// Existses the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Existses the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter, string includeProperties);

        /// <summary>
        /// Counts the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Counts the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> filter, string includeProperties);

        /// <summary>
        /// Counts the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Counts the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, string includeProperties);
    }
}