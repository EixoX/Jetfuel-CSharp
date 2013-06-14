using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text
{
    public static class Tokenizer
    {
        public static IEnumerable<KeyValuePair<int, string>> Tokenize(string content, string betweenLeft, string betweenRight)
        {
            int lpos = 0;
            int lLength = betweenLeft.Length;
            int rpos = 0;
            int rLength = betweenRight.Length;

            if (!string.IsNullOrEmpty(content))
            {
                lpos = content.IndexOf(betweenLeft, lpos) + lLength;
                while (lpos >= lLength)
                {
                    rpos = content.IndexOf(betweenRight, lpos);
                    if (rpos > lpos)
                    {
                        yield return new KeyValuePair<int, string>(lpos, content.Substring(lpos, rpos - lpos));
                        lpos = rpos + lLength;
                    }

                    lpos = content.IndexOf(betweenLeft, lpos) + lLength;
                }
            }
        }
    }
}
