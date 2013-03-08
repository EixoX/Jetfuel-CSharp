using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CpfOrCnpj : Attribute, Restriction
    {
        public static bool IsCpfOrCnpj(long value) { return Cpf.IsValid(value) || Cnpj.IsValid(value); }
        public static bool IsCpfOrCnpj(string value) { return IsCpfOrCnpj(long.Parse(Interceptors.DigitsOnly.Intercept(value))); }

        public bool Validate(object input)
        {
            return input == null ? true : (input is long || input is int ? IsCpfOrCnpj((long)input) : IsCpfOrCnpj(input.ToString()));
        }

        public string GetRestrictionMessage(object input)
        {
            return "Not a valid cpf or cnpj";
        }
    }
}
