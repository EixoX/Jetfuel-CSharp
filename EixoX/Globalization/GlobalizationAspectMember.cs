using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Globalization
{
    public class GlobalizationAspectMember
        : AspectMember
    {
        private readonly Dictionary<int, Dictionary<string, string>> _Elements;

        public GlobalizationAspectMember(ClassAcessor acessor)
            : base(acessor)
        {
            this._Elements = new Dictionary<int, Dictionary<string, string>>();
        }

        public string GetItem(string name, int lcid)
        {
            Dictionary<string, string> dic;
            if (_Elements.TryGetValue(lcid, out dic))
            {
                string value;
                dic.TryGetValue(name, out value);
                return value;
            }
            else
            {
                return null;
            }
        }

        public string GetItem(string name)
        {
            return GetItem(name, System.Globalization.CultureInfo.CurrentUICulture.LCID);
        }

        public string GetItem(string name, string culture)
        {
            return GetItem(name, System.Globalization.CultureInfo.GetCultureInfo(culture).LCID);
        }


        public void SetItem(string name, int lcid, string value)
        {
            Dictionary<string, string> dic;
            if (_Elements.TryGetValue(lcid, out dic))
            {
                if (dic.ContainsKey(name))
                    dic[name] = value;
                else
                    dic.Add(name, value);
            }
            else
            {
                dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add(name, value);
                _Elements.Add(lcid, dic);
            }

        }

    }
}
