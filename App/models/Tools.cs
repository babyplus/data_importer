namespace MyModels
{
    public class Converter
    {
        public static DateTime UnixTimestampToRFC3339Nano(long unixTimestamp)
        {
            // Convert Unix timestamp to DateTime
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimestamp);
    
            // Format the DateTime object into RFC3339Nano format
            return dateTime;
        }

        private static DateTime ChangeDateTimeTimeZone(DateTime dateTime, string targetTimeZoneId)
        {
            // Convert to UTC if necessary
            DateTime utcDateTime = dateTime.ToUniversalTime();
        
            // Find the target timezone info
            TimeZoneInfo targetTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(targetTimeZoneId);
        
            // Convert to the target timezone
            DateTime convertedDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, targetTimeZoneInfo);
        
            return convertedDateTime;
        }

        public static long strToLong(string _)
        {
            return long.Parse(_);
        } 

        public static double strToDouble(string _)
        {
            return double.Parse(_);
        }
 
        public static long strToBool(string _)
        {
            return ("true" == _) ? 1 : 0;
        } 

    }
}
