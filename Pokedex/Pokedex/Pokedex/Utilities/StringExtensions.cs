using System.Collections.Generic;
using System.Linq;

namespace Pokedex.Utilities
{
    public static class StringExtensions
    {
        public static string UppercaseFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static IEnumerable<string> ToList(this string s)
        {
            return s.Split(new char[] { ',' }).Select(x => x.ToLower());
        }
    }
}
