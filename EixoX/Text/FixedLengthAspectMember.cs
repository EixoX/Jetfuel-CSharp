using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EixoX.Text
{
    public class FixedLengthAspectMember
        : AspectMember
    {
        private readonly CultureInfo _CultureOverride;
        private readonly int _Offset;
        private readonly int _Length;
        private readonly Adapters.SimpleAdapter _Adapter;

        public FixedLengthAspectMember(ClassAcessor acessor, FixedLengthColumnAttribute colAttriute)
            : base(acessor)
        {

            this._CultureOverride = string.IsNullOrEmpty(colAttriute.CultureInfoOverride) ?
                null :
                System.Globalization.CultureInfo.GetCultureInfo(colAttriute.CultureInfoOverride);

            this._Adapter = colAttriute.ParserType == null ?
                Adapters.SimpleAdapters.CreateInstance(acessor.DataType, colAttriute.FormatString, this._CultureOverride) :
                (Adapters.SimpleAdapter)Activator.CreateInstance(colAttriute.ParserType);


            this._Offset = colAttriute.Offset;
            this._Length = colAttriute.Length;

        }

        public CultureInfo CultureInfoOverride { get { return this._CultureOverride; } }
        public int Offset { get { return this._Offset; } }
        public int Length { get { return this._Length; } }


        public string GetFormattedMember(object entity, CultureInfo cultureInfo)
        {
            object value = GetValue(entity);
            string content = null;

            if (value == null)
            {
                content = new string(' ', _Length);
            }
            else
            {
                content = _Adapter.FormatObject(value, _CultureOverride == null ? cultureInfo : _CultureOverride);

                if (content.Length > _Length)
                    content = content.Substring(0, _Length);
                else if (content.Length < _Length)
                    content += new string(' ', _Length - content.Length);

            }
            return content;
        }

        public void SetFormattedMember(object entity, CultureInfo cultureInfo, string content)
        {
            content = content.Trim();

            if (string.IsNullOrEmpty(content))
            {
                if (this.DataType == PrimitiveTypes.String)
                    this.SetValue(entity, content);
            }
            else
            {
                object value = _Adapter.ParseObject(content, _CultureOverride == null ? cultureInfo : _CultureOverride);
                this.SetValue(entity, value);
            }
        }

        public void PutFormattedMember(object entity, CultureInfo cultureInfo, char[] buffer)
        {
            object value = GetValue(entity);
            if (value == null)
            {
                for (int i = 0; i < _Length; i++)
                    buffer[_Offset + i] = ' ';
            }
            else
            {
                string content = _Adapter.FormatObject(value, _CultureOverride == null ? cultureInfo : _CultureOverride);

                int imax = content.Length > _Length ? _Length : content.Length;
                for (int i = 0; i < imax; i++)
                    buffer[_Offset + i] = content[i];
                for (int i = content.Length; i < _Length; i++)
                    buffer[_Offset + i] = ' ';
            }
        }

    }
}
