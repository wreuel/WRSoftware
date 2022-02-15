using System;

namespace WRSoftware.Utils.Common.Providers.Interfaces
{
    public interface IDateTimeProvider
    {
        public DateTimeOffset DatetTimeOffsetNow { get; }

        public DateTime DateTimeNow { get; }
    }
}
