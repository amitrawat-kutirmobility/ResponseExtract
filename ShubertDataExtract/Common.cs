using System.Text.RegularExpressions;

namespace ShubertDataExtract
{
    public static class Common
    {
        public static string GetTextByRegexPattern(this string contentText, string pattern)
        {
            var match = Regex.Match(contentText, pattern);

            if (!match.Success)
            {
                return null;
            }

            return match.Groups[1].Value;
        }
    }
}
