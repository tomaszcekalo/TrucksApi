namespace TrucksApi
{
    public interface IDateTimeProvider
    {
        DateTime GetNow();
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeProvider()
        { }

        public DateTime GetNow()
        { return DateTime.UtcNow; }
    }
}