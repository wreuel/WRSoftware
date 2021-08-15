using WRSoftware.Utils.Common.Interfaces;

namespace WRSoftware.Utils.Common.Models
{
    /// <summary>
    /// Context of Service, used on logger and others
    /// </summary>
    /// <seealso cref="WRSoftware.Utils.Common.Interfaces.IServiceContext" />
    public class ServiceContext : IServiceContext
    {
        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public string Service { get; set; }

        /// <summary>
        /// Gets or sets the operation.
        /// </summary>
        /// <value>
        /// The operation.
        /// </value>
        public string Operation { get; set; }
    }
}