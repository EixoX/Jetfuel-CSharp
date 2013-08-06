using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Restrictions
{
    public class RestrictionMessages
    {
        private static Dictionary<string, Dictionary<int, string>> _Messages;

        private static Dictionary<string, Dictionary<int, string>> LazyLoadMessages()
        {
            if (_Messages != null)
                return _Messages;

            _Messages = new Dictionary<string, Dictionary<int, string>>();

            string fileName = System.IO.Path.GetDirectoryName(typeof(RestrictionMessages).Assembly.CodeBase).Replace("file:\\", "");
            fileName = System.IO.Path.Combine(fileName, "Restrictions");
            fileName = System.IO.Path.Combine(fileName, "RestrictionMessages.xml");

            if (System.IO.File.Exists(fileName))
            {
                System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                xdoc.Load(fileName);
                System.Xml.XmlElement root = xdoc["RestrictionMessages"];
                if (root != null)
                {
                    foreach (System.Xml.XmlElement message in root.GetElementsByTagName("Message"))
                    {
                        string key = message.GetAttribute("for");
                        if (!string.IsNullOrEmpty(key))
                        {
                            int lcid = int.Parse(message.GetAttribute("lcid"));
                            if (lcid > 0)
                            {
                                string content = message.InnerText;

                                Dictionary<int, string> local;
                                if (_Messages.TryGetValue(key, out local))
                                {
                                    if (local.ContainsKey(lcid))
                                        local[lcid] = content;
                                    else
                                        local.Add(lcid, content);
                                }
                                else
                                {
                                    local = new Dictionary<int, string>();
                                    local.Add(lcid, content);
                                    _Messages.Add(key, local);
                                }
                            }
                        }
                    }
                }
            }

            return _Messages;
            
        }

        public static string GetMessage(Restriction restriction, int lcid, object input)
        {
            Dictionary<string, Dictionary<int, string>> msgs = LazyLoadMessages();
            Dictionary<int, string> local;
            string msg;

            if (msgs.TryGetValue(restriction.GetType().FullName, out local))
            {
                if (local.TryGetValue(lcid, out msg))
                    return string.Format(msg, input);
                else
                    return string.Format(restriction.RestrictionMessageFormat, input);
            }
            else
                return string.Format(restriction.RestrictionMessageFormat, input);
        }
    }
}
