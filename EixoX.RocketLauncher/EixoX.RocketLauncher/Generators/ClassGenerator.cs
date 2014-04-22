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

        /// <summary>
        /// Creates a class file, based on a table and it's columns
        /// </summary>
        /// <param name="table">the database table to be based in</param>
        /// <param name="programmingLanguage">the programming language used (some may differ from one another)</param>
        /// <returns></returns>
        public virtual ClassFile CreateClass(GenericDatabaseTable table, ProgrammingLanguage programmingLanguage)
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

                // Adding restriction annotation
                if (column.MaxLength > 0)
                    annotations.Add(Annotations.MaxLength.Replace("{{value}}", column.MaxLength.ToString()));

                // Adding UI annotations
                if (column.IsIdentity)
                    annotations.Add(Annotations.UIHidden);
                else
                    annotations.Add(Annotations.UISingleLine);

                // Adding database annotations
                if (column.IsIdentity)
                    annotations.Add(Annotations.DatabaseIdentityColumn);
                else if (column.Name.Equals("DateCreated", StringComparison.OrdinalIgnoreCase))
                    annotations.Add(Annotations.DateGeneratorInsert);
                else if (column.Name.Equals("DateUpdated", StringComparison.OrdinalIgnoreCase))
                    annotations.Add(Annotations.DateGeneratorUpdate);
                else
                {
                    int columnNameAsInteger;
                    if (int.TryParse(column.Name, out columnNameAsInteger))
                    {
                        annotations.Add(Annotations.CustomDatabaseColumn(column.Name));
                        column.Name = "C_" + columnNameAsInteger;
                    }
                    else
                        annotations.Add(Annotations.DatabaseColumn);
                }

                attributes.Add(attributeTemplate
                    .Replace("{{annotations}}", String.Join("\n\t\t", annotations))
                    .Replace("{{datatype}}", column.DataType.Name.ToString())
                    .Replace("{{name}}", column.Name)
                    .Replace("{{visibility}}", "\t\tpublic"));
            }

            file.FileContent = classTemplate
                .Replace("{{namespace}}", table.DatabaseName)
                .Replace("{{classname}}", table.Name)
                .Replace("{{attributes}}", String.Join("\n\n\t\t", attributes));

            return file;
        }

        private string ClassFileName(string tableName)
        {
            return tableName + GetClassExtension();
        }
    }
}
