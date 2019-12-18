using System.Globalization;
using System.Linq;
using System.Text;

namespace Rise.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToNeutral(this string text)
        {
            var formD = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var ch in from ch in formD let uc = CharUnicodeInfo.GetUnicodeCategory(ch) where uc != UnicodeCategory.NonSpacingMark select ch)
            {
                sb.Append(ch);
            }

            return sb.ToString().ToUpper().Normalize(NormalizationForm.FormC);
        }
    }
}