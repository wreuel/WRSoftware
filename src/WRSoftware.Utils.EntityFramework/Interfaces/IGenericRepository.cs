using WRSoftware.Utils.EntityFrameworkCore.Interfaces.CQRS;

namespace WRSoftware.Utils.EntityFrameworkCore.Interfaces
{
    /// <summary>
    /// Interface of the CQRS as one Unique Repository
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="WRSoftware.Utils.EntityFrameworkCore.Interfaces.CQRS.ICommandRepository{TEntity}" />
    /// <seealso cref="WRSoftware.Utils.EntityFrameworkCore.Interfaces.CQRS.IQueryRepository{TEntity}" />
    public interface IGenericRepository<TEntity> : ICommandRepository<TEntity>, IQueryRepository<TEntity>
        where TEntity : class
    {
    }
}