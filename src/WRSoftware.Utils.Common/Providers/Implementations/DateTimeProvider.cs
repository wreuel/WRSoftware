using System;
using WRSoftware.Utils.Common.Providers.Interfaces;

namespace WRSoftware.Utils.Common.Providers.Implementations
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset DatetTimeOffsetNow => DateTimeOffset.Now;

        public DateTime DateTimeNow => DateTime.Now;
    }
}
