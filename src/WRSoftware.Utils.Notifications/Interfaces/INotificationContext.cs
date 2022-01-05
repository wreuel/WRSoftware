using System;
using System.Collections.Generic;
using WRSoftware.Utils.Common.Models.Errors;

namespace WRSoftware.Utils.Notifications.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface INotificationContext
    {
        /// <summary>
        /// Gets the notifications.
        /// </summary>
        /// <value>
        /// The notifications.
        /// </value>
        IList<Error> Notifications { get; }
       
        /// <summary>
        /// Gets a value indicating whether this instance has notifications.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has notifications; otherwise, <c>false</c>.
        /// </value>
        bool HasNotifications { get; }

        /// <summary>
        /// Adds the specified error.
        /// </summary>
        /// <param name="error">The error.</param>
        void Add(Error error);

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="errors">The errors.</param>
        void AddRange(IEnumerable<Error> errors);

        /// <summary>
        /// Clears the notifications.
        /// </summary>
        void ClearNotifications();

        /// <summary>
        /// Clears the notifications.
        /// </summary>
        /// <param name="type">The type.</param>
        void ClearNotifications(Type type);
    }
}
