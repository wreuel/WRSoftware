using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WRSoftware.Utils.Common.IServices
{
    /// <summary>
    /// Interface to be implemented by the services so, .
    /// the BuildPredicate could be user checking the filter
    /// and then a Linq query can be made
    /// </summary>
    /// <typeparam name="DataBaseObject">The type of the ata base object.</typeparam>
    /// <typeparam name="CreateObj">The type of the reate object.</typeparam>
    /// <typeparam name="EditObj">The type of the dit object.</typeparam>
    /// <typeparam name="GetObj">The type of the et object.</typeparam>
    /// <typeparam name="Filter">The type of the ilter.</typeparam>
    /// <typeparam name="Response">The type of the esponse.</typeparam>
    /// <typeparam name="Paginated">The type of the aginated.</typeparam>
    public interface IBaseService<DataBaseObject, CreateObj, EditObj, GetObj, Filter, Response, Paginated>
    {
        /// <summary>
        /// Creates the specified create.
        /// </summary>
        /// <param name="create">The create.</param>
        /// <returns></returns>
        Task<Response> Create(CreateObj create);

        /// <summary>
        /// Edits the specified edit.
        /// </summary>
        /// <param name="edit">The edit.</param>
        /// <returns></returns>
        Task<Response> Edit(EditObj edit);

        /// <summary>
        /// Gets the specified get.
        /// </summary>
        /// <param name="get">The get.</param>
        /// <returns></returns>
        Task<Response> Get(GetObj get);

        /// <summary>
        /// Paginates the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<Paginated> Paginate(Filter filter);

        /// <summary>
        /// Builds the predicate.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Expression<Func<DataBaseObject, bool>> BuildPredicate(Filter filter);
    }
}
