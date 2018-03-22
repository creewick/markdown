using System.Text;

namespace Markdown
{
    public static class StringExtensions
    {
        public static string EscapedSubstring(this string text, int startIndex, int endIndex)
        {
            var result = new StringBuilder();

            for (var i = startIndex; i < endIndex; i++)
            {
                result.Append(text[i]);
                if (text[i] == '\\' && i + 1 < text.Length && text[i + 1] == '\\')
                    i++;
            }
            
            return result.ToString();
        }
        
        public static bool SubstringMatch(this string text, int index, string expected)
        {
            return index + expected.Length <= text.Length &&
                   text.Substring(index, expected.Length) == expected;
        }

        public static bool IsPrevSpace(this string text, Tag tag) 
            => tag.Index == 0 || text[tag.Index - 1] == ' ';

        public static bool IsNextSpace(this string text, Tag tag)
        {
            var nextIndex = tag.Index + tag.Code.Length;
            return nextIndex >= text.Length || text[nextIndex] == ' ';
        }

        public static bool IsEscaped(this string text, Tag tag)
        {
            return tag.Index > 0 && text[tag.Index - 1] == '\\' && 
                   (tag.Index == 1 || text[tag.Index - 2] != '\\');
        }

        public static bool IsDigitNear(this string text, Tag tag)
        {
            var nextIndex = tag.Index + tag.Code.Length;
            return tag.Index > 0 && char.IsDigit(text[tag.Index - 1]) ||
                   nextIndex < text.Length && char.IsDigit(text[nextIndex]);
        }
    }
}