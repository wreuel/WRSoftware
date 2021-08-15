namespace WRSoftware.Utils.Common.Interfaces
{
    /// <summary>
    /// The Interface of Service Context
    /// </summary>
    public interface IServiceContext
    {
        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        string Service { get; set; }

        /// <summary>
        /// Gets or sets the operation.
        /// </summary>
        /// <value>
        /// The operation.
        /// </value>
        string Operation { get; set; }
    }
}