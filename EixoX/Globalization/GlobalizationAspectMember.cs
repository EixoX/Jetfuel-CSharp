using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Globalization
{
    public class GlobalizationAspectMember
        : AspectMember, IGlobalization
    {
        private readonly GlobalizationList _Terms;

        public GlobalizationAspectMember(ClassAcessor acessor)
            : base(acessor)
        {
            this._Terms = new GlobalizationList();
        }

        public GlobalizationList Terms { get { return this._Terms; } }

        public string GetTerm(string name)
        {
            return _Terms.GetTerm(name);
        }

        public string GetTerm(string name, int lcid)
        {
            return _Terms.GetTerm(name, lcid);
        }

        public string GetTerm(string name, string culture)
        {
            return _Terms.GetTerm(name, culture);
        }

        public string GetTerm(string name, System.Globalization.CultureInfo culture)
        {
            return _Terms.GetTerm(name, culture);
        }

        public void SetTerm(string name, string value)
        {
            _Terms.SetTerm(name, value);
        }

        public void SetTerm(string name, int lcid, string value)
        {
            _Terms.SetTerm(name, lcid, value);
        }

        public void SetTerm(string name, string culture, string value)
        {
            _Terms.SetTerm(name, culture, value);
        }

        public void SetTerm(string name, System.Globalization.CultureInfo culture, string value)
        {
            _Terms.SetTerm(name, culture, value);
        }

        public int Count
        {
            get { return _Terms.Count; }
        }

        public IEnumerable<KeyValuePair<string, string>> GetTerms(int lcid)
        {
            return _Terms.GetTerms(lcid);
        }

        public IEnumerable<KeyValuePair<string, string>> GetTerms(string culture)
        {
            return _Terms.GetTerms(culture);
        }

        public IEnumerable<KeyValuePair<string, string>> GetTerms(System.Globalization.CultureInfo culture)
        {
            return _Terms.GetTerms(culture);
        }
    }
}
