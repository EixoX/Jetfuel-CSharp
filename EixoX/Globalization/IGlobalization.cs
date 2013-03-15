using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EixoX.Globalization
{
    public interface IGlobalization
    {

        string GetTerm(string name);
        string GetTerm(string name, int lcid);
        string GetTerm(string name, string culture);
        string GetTerm(string name, CultureInfo culture);

        void SetTerm(string name, string value);
        void SetTerm(string name, int lcid, string value);
        void SetTerm(string name, string culture, string value);
        void SetTerm(string name, CultureInfo culture, string value);

        int Count { get; }

        IEnumerable<KeyValuePair<string, string>> GetTerms(int lcid);
        IEnumerable<KeyValuePair<string, string>> GetTerms(string culture);
        IEnumerable<KeyValuePair<string, string>> GetTerms(CultureInfo culture);
        
    }
}
