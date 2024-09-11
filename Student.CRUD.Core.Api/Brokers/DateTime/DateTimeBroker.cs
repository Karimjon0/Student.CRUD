
namespace StudentCRUD.Core.Api.Brokers.DateTimes
{
    public class DateTimeBroker : IDateTimeBroker
    {
        public DateTimeOffset GetCurrentFDateTimeOffset() =>
            DateTimeOffset.UtcNow;
    }
}