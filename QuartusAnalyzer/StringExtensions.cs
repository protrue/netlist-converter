using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartusAnalyzer
{
    public static class StringExtensions
    {
        public static string RemoveAll(this string text, params string[] stringsToRemove) =>
        stringsToRemove.Aggregate(text,
            (current, stringToRemove) => current.Replace(stringToRemove, string.Empty));

        public static string RemoveFirst(this string text, string stringToRemove)
        {
            var index = text.IndexOf(stringToRemove);

            if (index < 0)
                return text;

            var firstPart = text.Substring(0, index);
            var secondPart = text.Substring(index + 1);

            return firstPart + secondPart;
        }

        public static string SubstringBetween(this string text, string first, string second)
        {
            var startIndex = text.IndexOf(first) + 1;
            var endIndex = text.IndexOf(second);
            return text.Substring(startIndex, endIndex - startIndex);
        }
    }
}
