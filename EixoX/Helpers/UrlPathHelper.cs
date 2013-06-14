using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    /// <summary>
    /// Create a Url with an addition information in the end of it
    /// </summary>
    public class UrlPathHelper
    {
        public static string RewriteUrlPath(string View, string Controller, int Id, string addition)
        {
            StringBuilder sb = new StringBuilder(1024);

            addition = addition.Replace("&", "").Replace(" ", "-");
            addition = addition.Replace("--", "-");
            addition = addition.Replace(".", "");
            
            sb.Append("~/");
            sb.Append(View);
            sb.Append("/");
            sb.Append(Controller);
            sb.Append("/");
            sb.Append(Id);
            sb.Append("/");
            sb.Append(addition);

            return sb.ToString(); ;
        }

        public static string RewriteUrlPath(string View, string Controller, string addition)
        {
            StringBuilder sb = new StringBuilder(1024);

            addition = addition.Replace("&", "").Replace(" ", "-");
            addition = addition.Replace("--", "-");

            sb.Append("~/");
            sb.Append(View);
            sb.Append("/");
            sb.Append(Controller);
            sb.Append("/");
            sb.Append(addition);

            return sb.ToString(); ;
        }

        public static string RewriteUrlPath(string View, string addition)
        {
            StringBuilder sb = new StringBuilder(1024);

            addition = addition.Replace("&", "").Replace(" ", "-");
            addition = addition.Replace("--", "-");

            sb.Append("~/");
            sb.Append(View);
            sb.Append("/");
            sb.Append(addition);

            return sb.ToString(); ;
        }

        public static string GetFriendlyUrlName(string text)
        {
            int length = text.Length;
            bool addedLetter = false;
            StringBuilder builder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                if (char.IsLetterOrDigit(text, i))
                {
                    builder.Append(text[i]);
                    addedLetter = true;
                }
                else if (addedLetter && char.IsWhiteSpace(text, i))
                {
                    addedLetter = false;
                    builder.Append('-');
                }
            }
            return builder.ToString();
        }

    }
}
