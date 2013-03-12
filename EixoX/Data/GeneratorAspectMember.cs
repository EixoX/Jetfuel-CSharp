using System;


namespace EixoX.Data
{
    public class GeneratorAspectMember
        : AspectMember
    {
        private Generator _generator;

        public GeneratorAspectMember(ClassAcessor acessor)
            : base(acessor)
        {
            this._generator = GetAttribute<Generator>(true);
        }

        public Generator Generator { get { return _generator; } }
        public void ApplyTo(object entity, DataScope scope)
        {
            if (scope == _generator.GeneratorScope || _generator.GeneratorScope == DataScope.Save)
            {
                object value = GetValue(entity);
                if (ValidationHelper.IsNullOrEmpty(value))
                    SetValue(entity, _generator.Generate());
            }
        }
    }
}
