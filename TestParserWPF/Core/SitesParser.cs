using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParserWPF.Core
{
    public class SitesParser : IParser<List<string>>
    {
        public List<string> Parse(IHtmlDocument document)
        {
            var listContent = new List<string>();
            var tegA = document.QuerySelectorAll("a");

            foreach (var item in tegA)
            {
                listContent.Add(item.TextContent);
            }
            return listContent;
        }
    }
}
