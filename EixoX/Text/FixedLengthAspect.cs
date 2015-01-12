using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace EixoX.Text
{
    public class FixedLengthAspect
        : AbstractAspect<FixedLengthAspectMember>
    {
        private readonly CultureInfo _CultureInfo;
        private readonly int _LineWidth;

        protected override bool CreateAspectFor(ClassAcessor acessor, out FixedLengthAspectMember member)
        {
            FixedLengthColumnAttribute flca = acessor.GetAttribute<FixedLengthColumnAttribute>(true);
            if (flca == null)
            {
                member = null;
                return false;
            }
            else
            {
                member = new FixedLengthAspectMember(acessor, flca);
                return true;
            }
        }

        public FixedLengthAspect(Type dataType)
            : base(dataType)
        {
            FixedLengthTableAttribute flta = GetAttribute<FixedLengthTableAttribute>(true);
            if (flta != null)
            {
                if (!string.IsNullOrEmpty(flta.CultureInfo))
                    this._CultureInfo = System.Globalization.CultureInfo.GetCultureInfo(flta.CultureInfo);
                else
                    this._CultureInfo = System.Globalization.CultureInfo.InvariantCulture;
            }
            else
            {
                this._CultureInfo = System.Globalization.CultureInfo.InvariantCulture;
            }

            int maxWidth = 0;

            foreach (FixedLengthAspectMember flam in this)
            {
                if ((flam.Offset + flam.Length) > maxWidth)
                    maxWidth = flam.Offset + flam.Length;
            }

            this._LineWidth = maxWidth;

        }


        public int LineWidth { get { return this._LineWidth; } }
        public CultureInfo CultureInfo { get { return this._CultureInfo; } }


        public object Parse(string line)
        {
            object instance = Activator.CreateInstance(this.DataType);
            foreach (FixedLengthAspectMember member in this)
            {
                member.SetFormattedMember(instance, _CultureInfo, line.Substring(member.Offset, member.Length));
            }
            return instance;
        }

    }


    public class FixedLengthAspect<T>
        : FixedLengthAspect where T : new()
    {
        private FixedLengthAspect() : base(typeof(T)) { }

        private static FixedLengthAspect<T> _Instance;
        public static FixedLengthAspect<T> Instance { get { return _Instance ?? (_Instance = new FixedLengthAspect<T>()); } }


        public int WriteTo(System.IO.TextWriter output, IEnumerable<T> entities)
        {
            int lineWidth = this.LineWidth;
            if (lineWidth < 1)
                return 0;

            CultureInfo cultureInfo = CultureInfo;

            int counter = 0;
            char[] buffer = new char[lineWidth];
            foreach (T entity in entities)
            {
                foreach (FixedLengthAspectMember flam in this)
                    flam.PutFormattedMember(entity, cultureInfo, buffer);

                output.WriteLine(buffer);
                counter++;
            }
            return counter;
        }

        public int WriteTo(System.IO.TextWriter output, T entity)
        {
            int lineWidth = this.LineWidth;
            if (lineWidth < 1)
                return 0;

            CultureInfo cultureInfo = CultureInfo;

            char[] buffer = new char[lineWidth];
            foreach (FixedLengthAspectMember flam in this)
                flam.PutFormattedMember(entity, cultureInfo, buffer);

            output.WriteLine(buffer);
            return 1;
        }


        public string WriteString(IEnumerable<T> entities)
        {
            StringWriter sw = new StringWriter();
            WriteTo(sw, entities);
            return sw.ToString();
        }

        public string WriteString(T entity)
        {
            StringWriter sw = new StringWriter();
            WriteTo(sw, entity);
            return sw.ToString();
        }

        public IEnumerable<T> ReadFrom(IEnumerable<string> lines)
        {
            foreach (string line in lines)
            {
                T entity = new T();
                CultureInfo cinfo = this.CultureInfo;

                foreach (FixedLengthAspectMember flam in this)
                {
                    string content = line.Substring(flam.Offset, flam.Length);
                    flam.SetFormattedMember(entity, cinfo, content);
                }

                yield return entity;
            }
        }
    }
}
