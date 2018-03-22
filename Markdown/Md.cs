using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Markdown
{
   
    public class Md
    {
        public static string RenderToHtml(string markdown)
        {
            return ReplaceTags(markdown, GetOrderedTagsList(markdown));
        }

        private static string ReplaceTags(string markdown, IEnumerable<Tag> tags)
        {
            var result = new StringBuilder();
            var index = 0;
            foreach (var tag in tags)
            {
                result.Append(markdown.EscapedSubstring(index, tag.Index));
                var label = Tag.All[tag.Code];
                result.Append(tag.IsOpen
                    ? $"<{label}>"
                    : $"</{label}>");
                index = tag.Index + tag.Code.Length;
            }
            
            result.Append(markdown.EscapedSubstring(index, markdown.Length));
            return result.ToString();
        }
        
        private static IEnumerable<Tag> GetOrderedTagsList(string markdown)
        {
            var openedTags = new Stack<Tag>();
            var result = new List<Tag>();

            for (var i = 0; i < markdown.Length; i++)
            {
                if (!TryFindTag(markdown, i, out var tag))
                    continue;
                if (tag.CorrectOpenTag(markdown))
                    openedTags.Push(tag);
                else if (tag.CorrectCloseTag(markdown))
                    if (openedTags.EqualsTopTag(tag))
                    {
                        tag.IsOpen = false;
                        result.Add(openedTags.Pop());
                        result.Add(tag);
                    }
                    
                i += tag.Code.Length - 1;
            }

            return result.OrderBy(tag => tag.Index);
        }
        
        private static bool TryFindTag(string text, int index, out Tag tag)
        {
            var markdown = Tag.All.Keys
                .OrderBy(key => key.Length)
                .Reverse()
                .FirstOrDefault(key => text.SubstringMatch(index, key));
            tag = new Tag(markdown, index);
            return markdown != null;
        }
    }
}