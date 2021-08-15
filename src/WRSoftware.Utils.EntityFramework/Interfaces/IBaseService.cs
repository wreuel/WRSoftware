using System;
using System.Linq.Expressions;

namespace Utils.EntityFramework.Interfaces
{
    /// <summary>
    /// Interface to be implemented by the services so, .
    /// the BuildPredicate could be user checking the filter
    /// and then a Linq query can be made
    /// </summary>
    /// <typeparam name="DataBaseObject">The type of the ata base object.</typeparam>
    /// <typeparam name="Filter">The type of the ilter.</typeparam>
    public interface IBaseService<DataBaseObject, Filter>
    {
        /// <summary>
        /// Builds the predicate.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Expression<Func<DataBaseObject, bool>> BuildPredicate(Filter filter);
    }
}