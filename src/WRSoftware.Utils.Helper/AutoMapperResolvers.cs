using AutoMapper;
using System;

namespace WRSoftware.Utils.Helper
{
    /// <summary>
    /// Automapper Resolvers, which will contain Resolvers
    /// for data and others things
    /// </summary>
    public class AutoMapperResolvers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="AutoMapper.IValueResolver{System.Object, System.Object, System.DateTime}" />
        public class DateTimeResolver : IValueResolver<object, object, DateTime>
        {
            /// <summary>
            /// Implementors use source object to provide a destination object.
            /// </summary>
            /// <param name="source">Source object</param>
            /// <param name="destination">Destination object, if exists</param>
            /// <param name="destMember">Destination member</param>
            /// <param name="context">The context of the mapping</param>
            /// <returns>
            /// Result, typically build from the source resolution result
            /// </returns>
            public DateTime Resolve(object source, object destination, DateTime destMember, ResolutionContext context)
            {
                return DateTime.Now;
            }
        }

        public class DateTimeOffsetResolver : IValueResolver<object, object, DateTimeOffset>
        {

            /// <summary>
            /// Implementors use source object to provide a destination object.
            /// </summary>
            /// <param name="source">Source object</param>
            /// <param name="destination">Destination object, if exists</param>
            /// <param name="destMember">Destination member</param>
            /// <param name="context">The context of the mapping</param>
            /// <returns>
            /// Result, typically build from the source resolution result
            /// </returns>
            public DateTimeOffset Resolve(object source, object destination, DateTimeOffset destMember, ResolutionContext context)
            {
                return DateTimeOffset.Now;
            }
        }
    }
}