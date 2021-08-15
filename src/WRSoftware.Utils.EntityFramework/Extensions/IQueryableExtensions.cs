using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using WRSoftware.Utils.EntityFrameworkCore.Constants;

namespace WRSoftware.Utils.EntityFrameworkCore.Extensions
{
    /// <summary>
    /// Extestion for IQueryable
    /// </summary>
    internal static class IQueryableExtensions
    {
        /// <summary>
        /// Withes the include properties.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public static IQueryable<TEntity> WithIncludeProperties<TEntity>(this IQueryable<TEntity> query,
            string includeProperties) where TEntity : class
        {
            IQueryable<TEntity> source = query;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                string str = includeProperties;
                char[] separator = new char[1] { ',' };
                int num = 1;
                foreach (string navigationPropertyPath in str.Split(separator, (StringSplitOptions)num))
                    source = source.Include<TEntity>(navigationPropertyPath);
            }

            return source;
        }

        /// <summary>
        /// Withes the filter.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IQueryable<TEntity> WithFilter<TEntity>(this IQueryable<TEntity> query,
            Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            IQueryable<TEntity> source = query;
            if (filter == null)
                return source;
            return source.Where<TEntity>(filter);
        }

        /// <summary>
        /// Withes the order by.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="sortingDirection">The sorting direction.</param>
        /// <returns></returns>
        public static IQueryable<TEntity> WithOrderBy<TEntity, TKey>(this IQueryable<TEntity> query,
            Expression<Func<TEntity, TKey>> orderBy, ListSortDirection? sortingDirection) where TEntity : class
        {
            IQueryable<TEntity> source = query;
            if (orderBy == null)
                return source;
            if (sortingDirection.HasValue)
            {
                ///ListSortDirection? nullable = sortingDirection;
                ListSortDirection listSortDirection = ListSortDirection.Descending;
                if ((sortingDirection.GetValueOrDefault() == listSortDirection))
                    return (IQueryable<TEntity>)source.OrderByDescending<TEntity, TKey>(orderBy);
            }

            return (IQueryable<TEntity>)source.OrderBy<TEntity, TKey>(orderBy);
        }

        /// <summary>
        /// Withes the pagination.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public static IQueryable<TEntity> WithPagination<TEntity>(this IQueryable<TEntity> query, int? pageNumber,
            int? pageSize) where TEntity : class
        {
            IQueryable<TEntity> source = query;
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                //int num1 = pageNumber.Value <= 1 ? 0 : pageNumber.Value - 1;
                //int count1 = pageSize.Value > 0 ? pageSize.Value : Configs.DEFAULT_PAGE_SIZE;
                //int num2 = count1;
                //int count2 = num1 * num2;
                //source = Queryable.Take<TEntity>(Queryable.Skip<TEntity>(source, count2), count1);

                int pageNum = pageNumber.Value == 0 ? 1 : pageNumber.Value;

                int qtyPerPage = pageSize.Value > 0 ? pageSize.Value : Configs.DEFAULT_PAGE_SIZE;

                source = Queryable.Take<TEntity>(Queryable.Skip<TEntity>(source, (pageNum - 1) * qtyPerPage), qtyPerPage);
            }

            return source;
        }
    }
}