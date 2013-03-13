using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public class HtmlInputOptionEnum
        :HtmlInputOptionSource
    {
        private Type _Enum;

        public HtmlInputOptionEnum(Type e)
        {
            this._Enum = e;
        }

        public IEnumerable<HtmlInputOption> GetInputOptions()
        {
            foreach (object v in Enum.GetValues(_Enum))
                yield return new HtmlInputOption(v, Enum.GetName(_Enum, v));
        }
    }
}
