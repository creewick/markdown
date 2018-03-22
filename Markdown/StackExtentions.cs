using System.Collections.Generic;

namespace Markdown
{
    public static class StackExtenstions
    {
        public static bool EqualsTopTag(this Stack<Tag> stack, Tag tag)
        {
            return stack.Count > 0 && stack.Peek().Code == tag.Code;
        }
    }
}