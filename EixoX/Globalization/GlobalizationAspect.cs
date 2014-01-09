using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Globalization
{
    public class GlobalizationAspect
        : AbstractAspect<GlobalizationAspectMember>
    {
        protected override bool CreateAspectFor(ClassAcessor acessor, out GlobalizationAspectMember member)
        {
            member = new GlobalizationAspectMember(acessor);
            return true;
        }

        public GlobalizationAspect(Type dataType)
            : base(dataType)
        {

            
            string fileName = System.IO.Path.GetDirectoryName(dataType.Assembly.CodeBase);
            fileName = fileName.Replace("file:\\", "");
            fileName = System.IO.Path.Combine(fileName, "Globalization");
            fileName = System.IO.Path.Combine(fileName, dataType.FullName + ".xml");
            
            
            if (System.IO.File.Exists(fileName))
            {
                System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                xdoc.Load(fileName);
                System.Xml.XmlElement globalization = xdoc["Globalization"];
                if (globalization != null)
                {
                    foreach (System.Xml.XmlElement element in globalization.GetElementsByTagName("Aspect"))
                    {
                        int lcid = int.Parse(element.GetAttribute("culture"));
                        foreach (System.Xml.XmlElement child in element.ChildNodes)
                        {
                            string name = child.LocalName;
                            int ordinal = GetOrdinal(name);
                            if (ordinal >= 0)
                            {
                                GlobalizationAspectMember member = base[ordinal];
                                foreach (System.Xml.XmlAttribute attribute in child.Attributes)
                                    member.SetTerm(attribute.Name, lcid, attribute.Value);
                            }
                        }
                    }
                }
                    
            }

        }

    }

    public class GlobalizationAspect<T>
        : GlobalizationAspect
    {
        private static GlobalizationAspect<T> _Instance;
        private GlobalizationAspect() : base(typeof(T)) { }
        public static GlobalizationAspect<T> Instance
        {
            get { return _Instance ?? (_Instance = new GlobalizationAspect<T>()); }
        }
    }
}
