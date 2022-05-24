using System;
using System.Collections.Generic;
using System.Linq;

namespace WRSoftware.Utils.Common.Models
{
    /// <summary>
    /// The class that will give the Paginated data
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class Paginated<TEntity> where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Paginated{TEntity}"/> class.
        /// </summary>
        public Paginated()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Paginated{TEntity}" /> class.
        /// </summary>
        /// <param name="totalItemsCount">The total count.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="paginatedEntities">The paginated entities.</param>
        public Paginated(int totalItemsCount, int pageSize, int pageIndex, IEnumerable<TEntity> paginatedEntities)
        {
            TotalItemsCount = totalItemsCount; //total
            PageSize = pageSize; //pageLength
            TotalPages = (int)Math.Ceiling((double)TotalItemsCount / (double)pageSize); //TotalPages
            PageIndex = pageIndex == 0 ? 1 : pageIndex;
            PaginatedEntities = paginatedEntities;

            //End = pageIndex + PageSize / 2;
            Start = 1;
            End = TotalPages;

            var paginatedCount = paginatedEntities.Count();

            HasPreviousPage = PageIndex > 1;
            HasNextPage = PageIndex == 0 ? PageIndex + 1 < TotalPages : PageIndex < TotalPages;
            IsLastPage = PageIndex == 0 ? PageIndex + 1 == TotalPages : PageIndex == TotalPages;
            IsFirstPage = PageIndex == 0 || PageIndex == 1;

            if (IsLastPage)
            {
                To = totalItemsCount;
                From = totalItemsCount - paginatedCount;
            }
            else
            {
                To = PageIndex * paginatedCount;
                From = To - paginatedCount;
            }
            if (From == 0)
            {
                From = 1;
            }
        }

        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; private set; }

        /// <summary>
        /// Gets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public int Start { get; private set; }

        /// <summary>
        /// Gets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public int End { get; private set; }

        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>
        /// The index of the page.
        /// </value>
        public int PageIndex { get; private set; }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>
        /// The total pages.
        /// </value>
        public int TotalPages { get; private set; }

        /// <summary>
        /// Gets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public int TotalItemsCount { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance has previous page.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has previous page; otherwise, <c>false</c>.
        /// </value>
        public bool HasPreviousPage
        {
            //get { return PageIndex > 1; }
            get; private set;

        }

        /// <summary>
        /// Gets a value indicating whether this instance has next page.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has next page; otherwise, <c>false</c>.
        /// </value>
        public bool HasNextPage
        {
            //get { return PageIndex == 0 ? PageIndex + 1 < TotalPages : PageIndex < TotalPages; }
            get; private set;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is last page.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is last page; otherwise, <c>false</c>.
        /// </value>
        public bool IsLastPage
        {
            //get { return PageIndex == 0 ? PageIndex + 1 == TotalPages : PageIndex == TotalPages; }
            get; private set;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is first page.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is first page; otherwise, <c>false</c>.
        /// </value>
        public bool IsFirstPage
        {
            //get { return PageIndex == 0 || PageIndex == 1; }
            get; private set;
        }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public int From { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public int To { get; set; }

        /// <summary>
        /// Gets the paginated entities.
        /// </summary>
        /// <value>
        /// The paginated entities.
        /// </value>
        public IEnumerable<TEntity> PaginatedEntities { get; private set; }
    }
}