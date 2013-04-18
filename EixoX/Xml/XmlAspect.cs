using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;


namespace EixoX.Xml
{
    public class XmlAspect
        : AbstractAspect<XmlAspectMember>
    {

        private readonly string _XmlName;
        private readonly CultureInfo _Culture;

        protected XmlAspect(Type dataType)
            : base(dataType)
        {
            XmlAttribute xa = GetAttribute<XmlAttribute>(true);
            if (xa == null)
            {
                this._XmlName = dataType.Name;
                this._Culture = CultureInfo.InvariantCulture;
            }
            else
            {
                this._XmlName = xa.Name;
                this._Culture =
                    string.IsNullOrEmpty(xa.Culture) ?
                    CultureInfo.InvariantCulture :
                    CultureInfo.GetCultureInfo(xa.Culture);
            }
        }

        protected override bool CreateAspectFor(ClassAcessor acessor, out XmlAspectMember member)
        {
            XmlAttribute xa = acessor.GetAttribute<XmlAttribute>(true);
            if (xa == null)
            {
                member = null;
                return false;
            }
            else
            {
                member = new XmlAspectMember(
                    acessor,
                    xa.XmlType,
                    xa.Name,
                    string.IsNullOrEmpty(xa.Culture) ?
                    null :
                    System.Globalization.CultureInfo.GetCultureInfo(xa.Culture));

                return true;
            }

        }


        private static Dictionary<Type, XmlAspect> _Instances;

        public static XmlAspect GetInstance(Type dataType)
        {
            XmlAspect aspect;

            if (_Instances == null)
            {
                _Instances = new Dictionary<Type, XmlAspect>();
                aspect = new XmlAspect(dataType);
                _Instances.Add(dataType, aspect);
            }
            else
            {
                if (!_Instances.TryGetValue(dataType, out aspect))
                {
                    aspect = new XmlAspect(dataType);
                    _Instances.Add(dataType, aspect);
                }
            }

            return aspect;
        }

        public string XmlName
        {
            get { return this._XmlName; }
        }

        public CultureInfo Culture
        {
            get { return this._Culture; }
        }


        public void ReadXml(object entity, XmlElement element)
        {
            if (_XmlName.Equals(element.Name, StringComparison.OrdinalIgnoreCase))
            {
                foreach (XmlAspectMember member in this)
                {
                    switch (member.XmlType)
                    {
                        case XmlType.Attribute:
                            member.SetValue(
                                entity,
                                element.GetAttribute(member.XmlName),
                                member.Culture == null ? _Culture : member.Culture);
                            break;
                        case XmlType.Element:
                            //Parse a primitive type
                            if (member.DataType.IsPrimitive)
                            {
                                XmlElement child = element[member.XmlName];
                                if (child == null)
                                    member.SetValue(entity, null);
                                else
                                {
                                    member.SetValue(
                                        entity,
                                        child.InnerText,
                                        member.Culture == null ? _Culture : member.Culture);
                                }
                            }
                            //Parse a collection of items.
                            else if (typeof(System.Collections.ICollection).IsAssignableFrom(member.DataType))
                            {
                                System.Collections.ICollection collection = (System.Collections.ICollection)member.GetValue(entity);

                            }
                            break;
                        default:
                            throw new ArgumentException("Unknown xml type " + member.XmlType);
                    }
                }
            }
            else
                throw new ArgumentException("Expected element with name " + _XmlName + " and got " + element.Name);
        }

        public object ReadXml(XmlElement element)
        {
            object entity = Activator.CreateInstance(this.DataType);
            ReadXml(entity, element);
            return entity;
        }
    }


    public class XmlAspect<T> : XmlAspect
    {
        private XmlAspect() : base(typeof(T)) { }

        private static XmlAspect<T> _Instance;

        public static XmlAspect<T> Instance
        {
            get { return _Instance ?? (_Instance = new XmlAspect<T>()); }
        }
    }



    [Xml(Xml.XmlType.Element, "Cartao")]
    public class Cartao
    {
        [Xml]
        public int Numero;
        [Xml]
        public string Nome;
    }


    [Xml]
    public class Carteira
        : List<Cartao>
    {
        [Xml]
        public string Marca;
        [Xml]
        public string Modelo;
        [Xml]
        public string Tamanho;
    }


}
