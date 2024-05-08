namespace SitecoreSend.SDK.Tools;

public static class DateTimeTools
{
    public static bool IsDate(string? dateString)
    {
        if (dateString == null || string.IsNullOrEmpty(dateString) || dateString.Length < 8)
        {
            return false;
        }

        return dateString.StartsWith("/Date(") && dateString.EndsWith(")/");
    }
        
    public static DateTimeOffset Parse(string? dateString)
    {
        if (!IsDate(dateString))
        {
            return default;
        }
        // Extracting the timestamp value
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var startIndex = dateString.IndexOf('(') + 1;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        var endIndex = dateString.IndexOfAny(['+', ')']);
        if (!long.TryParse(dateString.Substring(startIndex, endIndex - startIndex), out var ticks))
        {
            return default;
        }
        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(ticks);

        // Extract the offset from the string, if present
        TimeSpan offset = TimeSpan.Zero;
        if (dateString[endIndex] == '+')
        {
            var offsetString = dateString.Substring(endIndex + 1, 4);
            offset = TimeSpan.ParseExact(offsetString, "hhmm", null);
        }

        // Apply the offset to the DateTimeOffset
        dateTimeOffset = new DateTimeOffset(dateTimeOffset.DateTime, offset);

        return dateTimeOffset;
    }

    public static string ToString(DateTimeOffset date)
    {
        return $"/Date({date.ToUnixTimeMilliseconds()})/";
    }
}