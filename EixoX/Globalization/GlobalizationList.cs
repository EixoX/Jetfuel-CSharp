using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EixoX.Globalization
{
    public class GlobalizationList
        : Dictionary<int, Dictionary<string, string>>, IGlobalization
    {


        public string GetTerm(string name)
        {
            return GetTerm(name, CultureInfo.CurrentUICulture.LCID);
        }

        public string GetTerm(string name, int lcid)
        {
            Dictionary<string, string> terms;
            string value;

            return TryGetValue(lcid, out terms) ?
                (terms.TryGetValue(name, out value) ? value : null) :
                null;
        }

        public string GetTerm(string name, string culture)
        {
            return GetTerm(name, CultureInfo.GetCultureInfo(culture));
        }

        public string GetTerm(string name, CultureInfo culture)
        {
            return GetTerm(name, culture.LCID);
        }

        public void SetTerm(string name, string value)
        {
            SetTerm(name, CultureInfo.CurrentUICulture, value);
        }

        public void SetTerm(string name, int lcid, string value)
        {
            Dictionary<string, string> terms;
            if (TryGetValue(lcid, out terms))
            {
                if (terms.ContainsKey(name))
                    terms[name] = value;
                else
                    terms.Add(name, value);
            }
            else
            {
                terms = new Dictionary<string, string>();
                terms.Add(name, value);
                base.Add(lcid, terms);
            }
        }

        public void SetTerm(string name, string culture, string value)
        {
            SetTerm(name, CultureInfo.GetCultureInfo(culture), value);
        }

        public void SetTerm(string name, CultureInfo culture, string value)
        {
            SetTerm(name, culture.LCID, value);
        }

        public IEnumerable<KeyValuePair<string, string>> GetTerms(int lcid)
        {
            return base[lcid];
        }

        public IEnumerable<KeyValuePair<string, string>> GetTerms(string culture)
        {
            return GetTerms(CultureInfo.GetCultureInfo(culture));
        }

        public IEnumerable<KeyValuePair<string, string>> GetTerms(System.Globalization.CultureInfo culture)
        {
            return GetTerms(culture.LCID);
        }
    }
}
