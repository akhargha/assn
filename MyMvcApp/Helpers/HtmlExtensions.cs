// File: HtmlExtensions.cs
// Location: Under a directory like /Helpers or /Utilities

using System.Text.RegularExpressions;

namespace MyMvcApp.Helpers
{
    public static class HtmlExtensions
    {
        public static string StripHtmlAndTruncate(this string input, int length)
        {
            if (string.IsNullOrEmpty(input)) return input;

            // Remove HTML tags
            var plainText = Regex.Replace(input, "<.*?>", string.Empty);

            // Truncate and add ellipsis if necessary
            return plainText.Length <= length ? plainText : plainText.Substring(0, length) + "...";
        }
    }
}
