using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace EixoX.Helpers
{
    public static class WebHelper
    {
        public static string PostTo(string url, params KeyValuePair<string, string>[] parameters)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";

            if (parameters != null && parameters.Length > 0)
            {
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    try
                    {
                        writer.Write(parameters[0].Key);
                        writer.Write('=');
                        writer.Write(Uri.EscapeUriString(parameters[0].Value));

                        for (int i = 1; i < parameters.Length; i++)
                        {
                            writer.Write('&');
                            writer.Write(parameters[i].Key);
                            writer.Write('=');
                            writer.Write(Uri.EscapeUriString(parameters[i].Value));
                        }
                    }
                    finally
                    {
                        writer.Close();
                    }
                }
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                try
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("ISO-8859-1")))
                    {
                        try
                        {
                            return reader.ReadToEnd();
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
                finally
                {
                    response.Close();
                }
            }

        }

        public static string StripHtmlAttributes(string content)
        {
            int l = content.Length;
            StringBuilder builder = new StringBuilder(l);
            bool isInTag = false;
            for (int i = 0; i < l; i++)
            {
                if (isInTag)
                {
                    if (Char.IsWhiteSpace(content[i]))
                    {
                        i = content.IndexOf('>', i) - 1;
                        isInTag = false;
                    }
                    else if (content[i] == '>')
                    {
                        builder.Append('>');
                        isInTag = false;
                    }
                    else
                    {
                        builder.Append(Char.ToLower(content[i]));
                    }
                }
                else if (content[i] == '<')
                {
                    isInTag = true;
                    builder.Append(content[i]);
                }
                else
                {
                    builder.Append(content[i]);
                }
            }

            return builder.ToString();
        }
    }
}
