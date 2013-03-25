using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    /// <summary>
    /// Generates a class based on a EixoX.RocketLauncher.GenericDatabaseTable
    /// 
    /// Class template:
    ///     using System;
    ///     using System.Collections.Generic;
    ///     using System.Linq;
    ///     using System.Text;
    ///     using System.Threading.Tasks;
    ///     
    ///     namespace {{namespace}}
    ///     {
    ///         [DatabaseTable]
    ///         class {{className}}
    ///         {
    ///             {{attributes}}
    ///         }        
    ///     }
    /// 
    /// Attribute template:
    ///     {{annotations}}
    ///     public {{datatype}} {{name}} { get; set; }
    /// </summary>
    public abstract class ClassGenerator
    {
        public abstract string GetClassExtension();

        public string _Directory { get; set; }

        public ClassGenerator(string directory)
        {
            _Directory = directory;
        }

        public ClassFile CreateClass(GenericDatabaseTable table, ProgrammingLanguage programmingLanguage)
        {
            ClassFile file = new ClassFile();
            file.FileName = ClassFileName(table.Name);

            string templateFolder = AppDomain.CurrentDomain.BaseDirectory + "Templates";
            string languagePrefix = Enum.GetName(typeof(ProgrammingLanguage), programmingLanguage);

            string classTemplate = File.ReadAllText(string.Concat(templateFolder, "\\", languagePrefix, "Class.eixox"));
            string attributeTemplate = System.IO.File.ReadAllText(string.Concat(templateFolder, "\\", languagePrefix, "Attribute.eixox"));

            List<string> attributes = new List<string>();
            foreach (GenericDatabaseColumn column in table.Columns)
            {
                List<string> annotations = new List<string>();
                annotations.Add(Annotations.DatabaseColumn);

                if (column.MaxLength > 0)
                    annotations.Add(Annotations.MaxLength.Replace("{{value}}", column.MaxLength.ToString()));

                if (column.Name.Equals("DateCreated", StringComparison.OrdinalIgnoreCase))
                    annotations.Add(Annotations.DateGeneratorInsert);
                else if (column.Name.Equals("DateUpdated", StringComparison.OrdinalIgnoreCase))
                    annotations.Add(Annotations.DateGeneratorUpdate);

                attributes.Add(attributeTemplate
                    .Replace("{{annotations}}", "\t\t" + String.Join("\n\t\t", annotations))
                    .Replace("{{datatype}}", column.DataType.Name.ToString())
                    .Replace("{{name}}", column.Name)
                    .Replace("{{visibility}}", "\t\tpublic"));
            }

            file.FileContent = classTemplate
                .Replace("{{namespace}}", table.DatabaseName)
                .Replace("{{classname}}", table.Name)
                .Replace("{{attributes}}", String.Join("\n\n", attributes));

            return file;
        }

        private string ClassFileName(string tableName)
        {
            return tableName + GetClassExtension();
        }
    }
}
