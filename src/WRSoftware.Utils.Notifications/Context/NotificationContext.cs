using System;
using System.Collections.Generic;
using System.Linq;
using WRSoftware.Utils.Common.Models.Errors;
using WRSoftware.Utils.Notifications.Interfaces;


namespace WRSoftware.Utils.Notifications.Context
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="WRSoftware.Utils.Notifications.Interfaces.INotification" />
    public class NotificationContext : INotification
    {
        /// <summary>
        /// Gets the notifications.
        /// </summary>
        /// <value>
        /// The notifications.
        /// </value>
        public IList<Error> Notifications { get; } = new List<Error>();

        /// <summary>
        /// Gets a value indicating whether this instance has notifications.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has notifications; otherwise, <c>false</c>.
        /// </value>
        public bool HasNotifications => Notifications.Any();

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IList<Error> Errors => Notifications.Where(x => x is Critical).ToList();

        /// <summary>
        /// Gets the warnings.
        /// </summary>
        /// <value>
        /// The warnings.
        /// </value>
        public IList<Error> Warnings => Notifications.Where(x => x is Warning).ToList();

        /// <summary>
        /// Gets the informations.
        /// </summary>
        /// <value>
        /// The informations.
        /// </value>
        public IList<Error> Informations => Notifications.Where(x => x is Information).ToList();

        /// <summary>
        /// Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has errors; otherwise, <c>false</c>.
        /// </value>
        public bool HasErrors => Notifications.Any(x => x is Critical);

        /// <summary>
        /// Gets a value indicating whether this instance has warnings.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has warnings; otherwise, <c>false</c>.
        /// </value>
        public bool HasWarnings => Notifications.Any(x => x is Warning);

        /// <summary>
        /// Gets a value indicating whether this instance has informations.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has informations; otherwise, <c>false</c>.
        /// </value>
        public bool HasInformations => Notifications.Any(x => x is Information);

        /// <summary>
        /// Adds the specified description.
        /// </summary>
        /// <param name="description">The description.</param>
        public void Add(Error description) => Notifications.Add(description);

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public void AddRange(IEnumerable<Error> errors) => ((List<Error>)Notifications).AddRange(errors);

        /// <summary>
        /// Clears the notifications.
        /// </summary>
        public void ClearNotifications() => Notifications.Clear();

        /// <summary>
        /// Clears the notifications.
        /// </summary>
        /// <param name="type">The type.</param>
        public void ClearNotifications(Type type) => ((List<Error>)Notifications).RemoveAll(n => n.GetType() == type);
    }
}
