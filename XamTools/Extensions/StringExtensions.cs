using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoTouchx.Extensions
{
    public static class StringExtensions
    {
        public static int ParseInt(this string value, int defaultIntValue = 0) => int.TryParse(value, out var parsedInt) ? parsedInt : defaultIntValue;

        public static int? ParseNullableInt(this string value) => string.IsNullOrEmpty(value) ? (int?)null : value.ParseInt();

        public static decimal ParseDec(this string value, decimal defaultIntValue) => decimal.TryParse(value, out var parsedInt) ? parsedInt : defaultIntValue;

        public static decimal? ParseDec(this string value) => string.IsNullOrEmpty(value) ? (decimal?)null : value.ParseDec(decimal.Zero);

        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);
    }
}
