using System;
using System.Collections.Generic;
using WRSoftware.Utils.Common.Models.Errors;

namespace WRSoftware.Utils.Notifications.Interfaces
{
    public interface INotification
    {
        IList<Error> Notifications { get; }
        bool HasNotifications { get; }

        void Add(Error error);
        void AddRange(IEnumerable<Error> errors);
        void ClearNotifications();
        void ClearNotifications(Type type);
    }
}
