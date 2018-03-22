using System.Collections.Generic;

namespace Markdown
{
    public struct Tag
    {
        public static Dictionary<string, string> All = new Dictionary<string, string>
        {
            {"__", "strong"},
            {"_", "em"}
        };

        public readonly string Code;
        public readonly int Index;
        public bool IsOpen;

        public Tag(string code, int index, bool isOpen=true)
        {
            Code = code;
            Index = index;
            IsOpen = isOpen;
        }
        
        public bool CorrectCloseTag(string text)
        {
            return !text.IsPrevSpace(this) &&
                   !text.IsEscaped(this) &&
                   !text.IsDigitNear(this);
        }
        
        public bool CorrectOpenTag(string text)
        {
            return !text.IsNextSpace(this) &&
                   !text.IsEscaped(this) &&
                   !text.IsDigitNear(this);
        }
    }
}