using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITKMS_business.Util
{
    public class Helper
    {
        public static string GetStringPart(string text, int topCharacters)
        {
            if (text.IndexOf("<p>") == 0)
                text = text.Remove(0, 3);
            text = text.Replace("<p>", "\n");
            text = text.Replace("<P>", "\n");
            text = text.Replace("<BR>", "\n");
            text = text.Replace("<BR />", "\n");
            text = text.Replace("<br>", "\n");
            text = text.Replace("<br />", "\n");
            text = HtmlRemoval.StripTagsCharArray(text);

            if (text.Length > topCharacters)
            {
                text = text.Substring(0, topCharacters);
                for (int i = 99; i > 0; i--)
                {
                    if (text[i] != ' ')
                        text = text.Remove(i, 1);
                    else
                        break;
                }
                text = text + " ...";
            }

            text = text.Replace("\n", "<br />");
            return text;
        }
    }
}
