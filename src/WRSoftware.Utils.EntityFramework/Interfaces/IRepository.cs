namespace WRSoftware.Utils.EntityFrameworkCore.Interfaces
{
    /// <summary>
    /// The Repository itself
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        IUnitOfWork UnitOfWork { get; }
    }
}