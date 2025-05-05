using System.Globalization;

namespace Airbnb.OrderManagement.Application.BoundedContext.Services;

public interface ITimeZoneConverter
{
    DateTime ToUtc(string localDateTime, string timeZoneId);
}

public class TimeZoneConverter : ITimeZoneConverter
{
    private const string DateTimeFormat = "MM.dd.yyyy HH:mm";

    public DateTime ToUtc(string localDateTime, string timeZoneId)
    {
        if (!DateTime.TryParseExact(localDateTime, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
            throw new FormatException($"Invalid date format: '{localDateTime}'. Expected format: {DateTimeFormat}");

        var unspecified = DateTime.SpecifyKind(parsed, DateTimeKind.Unspecified);

        TimeZoneInfo tz;
        try
        {
            tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }
        catch (TimeZoneNotFoundException)
        {
            throw new ArgumentException($"Unknown time zone ID: '{timeZoneId}'");
        }

        return TimeZoneInfo.ConvertTimeToUtc(unspecified, tz);
    }
}