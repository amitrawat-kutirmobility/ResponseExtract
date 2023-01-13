using HtmlAgilityPack;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

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

        /// <summary>
        /// Gets the text from HTML by x path.
        /// </summary>
        /// <param name="htmlText">The HTML text.</param>
        /// <param name="xPath">The x path.</param>
        /// <returns>String or Null</returns>
        public static string? GetTextFromHtmlByXPath(this string htmlText, string xPath)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlText);
            var element = doc.DocumentNode.SelectSingleNode(xPath);

            if (element == null || string.IsNullOrEmpty(element?.InnerText))
            {
                return null;
            }

            return WebUtility.HtmlDecode(element.InnerText.Trim());
        }

        public static string? GetTextByXPath(this HtmlDocument doc, string xPath)
        {
            if (doc == null)
            {
                return null;
            }

            var element = doc.DocumentNode.SelectSingleNode(xPath);

            if (element == null || string.IsNullOrEmpty(element?.InnerText))
            {
                return null;
            }

            return WebUtility.HtmlDecode(element.InnerText.Trim());
        }

        public static string? ConvertHtmlContentToText(this string htmlContent)
        {
            if(string.IsNullOrEmpty(htmlContent))
            {
                return null;
            }

            htmlContent = htmlContent
                .Replace("\r\n", "")
                .Trim();

            return WebUtility.HtmlDecode(htmlContent);
        }
    }
}
