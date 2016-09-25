using System;

namespace RMSniperFeeder
{
    public static class extensions
    {
        public static long ToUnixTimestamp(this DateTime value)
        {
            return (long)Math.Truncate((value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds * 1000);
        }
    }
}