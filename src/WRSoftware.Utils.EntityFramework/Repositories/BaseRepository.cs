using Microsoft.EntityFrameworkCore;
using WRSoftware.Utils.EntityFrameworkCore.Interfaces;

namespace WRSoftware.Utils.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// The Base repository
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="WRSoftware.Utils.EntityFrameworkCore.Interfaces.IRepository" />
    public abstract class BaseRepository<TEntity> : IRepository where TEntity : class
    {
        /// <summary>
        /// The context
        /// </summary>
        protected readonly BaseDbContext _context;

        /// <summary>
        /// The database set
        /// </summary>
        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected BaseRepository(BaseDbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        public IUnitOfWork UnitOfWork
        {
            get { return (IUnitOfWork)this._context; }
        }
    }
}