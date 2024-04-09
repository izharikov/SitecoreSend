namespace SitecoreSend.SDK.Tools
{
    internal static class DateTimeTools
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
            var timestampString = dateString.Substring(6, dateString.Length - 8);
        
            // Converting the timestamp to a long
            long timestamp;
            if (long.TryParse(timestampString, out timestamp))
            {
                // Creating a DateTimeOffset from the timestamp
                return DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
            }

            return default;
        }

        public static string ToString(DateTimeOffset date)
        {
            return $"/Date({date.ToUnixTimeMilliseconds()})/";
        }
    }
}