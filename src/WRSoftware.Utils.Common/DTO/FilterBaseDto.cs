namespace WRSoftware.Utils.Common.DTO
{
    /// <summary>
    /// The filter Generic Base,
    /// It will hold object of T where it will have 
    /// the info which will be filtered considering the 
    /// page Index and the Lenght
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FilterBaseDto<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterBaseDto{T}"/> class.
        /// </summary>
        public FilterBaseDto()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterBaseDto{T}"/> class.
        /// </summary>
        /// <param name="isFiltered">if set to <c>true</c> [is filtered].</param>
        /// <param name="data">The data.</param>
        /// <param name="index">The index.</param>
        /// <param name="length">The length.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="sortDirection">The sort direction. 0 = Ascending, 1 = Descending</param>
        public FilterBaseDto(bool isFiltered, T data, int index, int length, string sort, byte sortDirection = 0)
        {
            IsFiltered = isFiltered;
            Data = data;
            Index = index;
            Length = length;
            Sort = sort;
            SortDirection = sortDirection;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is filtered.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is filtered; otherwise, <c>false</c>.
        /// </value>
        public bool IsFiltered { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        public string Sort { get; set; }

        /// <summary>
        /// Gets or sets the sort direction. 0 = Ascending, 1 = Descending 
        /// </summary>
        /// <value>
        /// The sort direction.
        /// </value>
        public int SortDirection { get; set; }
    }
}