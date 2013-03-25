using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    /// <summary>
    /// Represents a class file
    /// </summary>
    public class ClassFile
    {
        public string FileName { get; set; }
        public string FileContent { get; set; }

        internal void Save(string path)
        {
            System.IO.File.WriteAllText(path + "\\" + FileName, this.FileContent);
        }
    }
}
