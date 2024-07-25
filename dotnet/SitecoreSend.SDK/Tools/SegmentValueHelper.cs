using System.Collections;
using System.Globalization;

namespace SitecoreSend.SDK.Tools;

public static class SegmentValueHelper
{
    public static string DateValue(DateTimeOffset date, string format = "yyyy-MM-dd")
    {
        return date.ToString(format);
    }

    public static DateTimeOffset ParseDate(string? value, string format = "yyyy-MM-dd")
    {
        return DateTimeOffset.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal,
            out var result)
            ? result
            : default;
    }

    public static string ListValue(IEnumerable list)
    {
        return string.Join("|", list);
    }

    public static string ListValue(params object[] list)
    {
        return string.Join("|", list);
    }

    public static string EnumValue<T>(params T[] list) where T : struct, Enum, IConvertible
    {
        return string.Join("|", list.Select(x => x.ToInt32(null)));
    }

    public static IList<string> Parse(string? value)
    {
        return value?.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries).ToList() ?? [];
    }

    public static IList<T> Parse<T>(string? value) where T : struct, Enum
    {
        return value?.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => Enum.TryParse(x, out T res) ? res : default)
            .ToList() ?? [];
    }
}