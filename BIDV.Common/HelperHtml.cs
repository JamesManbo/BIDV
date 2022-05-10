using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BIDV.Common
{
    public class HelperHtml
    {
        /// <summary>
        /// Remove Html in {"</p>", "<br/>", "<br />", "</div>", "</table>", "<nl/>", "<nl />"}
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveHtmlTags(string input)
        {
            try
            {
                // these are end tags that create a line break and need to be replaced with a space
                var lineBreakEndTags = new[] { "</p>", "<br/>", "<br />", "</div>", "</table>", "<nl/>", "<nl />" };

                if (!string.IsNullOrEmpty(input))
                {
                    // replace carriage return w/ space
                    input = input.Replace("\r", " ");

                    // replace newline w/ space
                    input = input.Replace("\n", " ");

                    // replace tab
                    input = input.Replace("\t", string.Empty);

                    // replace line break tags with a space
                    input = lineBreakEndTags.Aggregate(input, (current, tag) => current.Replace(tag, " "));

                    // replace all html tags
                    var regex = new Regex("<(.|\n)*?>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    input = regex.Replace(input, string.Empty);

                    // replace special html characters
                    var specialChars = new Dictionary<string, string>
                    {
                        {"\u00A0", " "},
                        {@"&nbsp;", " "},
                        {@"&quot;", "\""},
                        {@"&ldquo;", "\""},
                        {@"&rdquo;", "\""},
                        {@"&rsquo;", "'"},
                        {@"&lsquo;", "'"},
                        {@"&amp;", "&"},
                        {@"&ndash;", "-"},
                        {@"&mdash;", "-"},
                        {@"&lt;", "<"},
                        {@"&gt;", ">"},
                        {@"&lsaquo;", "<"},
                        {@"&rsaquo;", ">"},
                        {@"&trade;", "(tm)"},
                        {@"&frasl;", "/"},
                        {@"&copy;", "(c)"},
                        {@"&reg;", "(r)"},
                        {@"&iquest;", "?"},
                        {@"&iexcl;", "!"},
                        {@"&bull;", "*"}
                    };

                    input = specialChars.Aggregate(input, (current, specialChar) => current.Replace(specialChar.Key, specialChar.Value));

                    // any other special char is deleted
                    input = Regex.Replace(input, @"&#[^ ;]+;", string.Empty);
                    input = Regex.Replace(input, @"&[^ ;]+;", string.Empty);

                    // remove extra duplicate spaces
                    input = Regex.Replace(input, @"( )+", " ");

                    // trim
                    input = input.Trim();
                }
            }
            catch
            {
                //Log.WriteError("zsdgsfthsaeaest", ex, "string that failed to be stripped of html: " + input);
            }
            return input;
        }
        /// <summary>
        /// Remove all Html
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Strip(string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }

    }
}