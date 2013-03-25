using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    public class CSharpClassGenerator : ClassGenerator
    {
        public CSharpClassGenerator()
            : base(String.Empty)
        {

        }

        public CSharpClassGenerator(string dir)
            : base(dir)
        {

        }

        public void SetDirectory()
        {
        }

        public override string GetClassExtension()
        {
            return ".cs";
        }
    }
}
