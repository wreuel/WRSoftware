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
    }
}