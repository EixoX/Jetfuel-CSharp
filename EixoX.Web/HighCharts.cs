using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EixoX;
using EixoX.Collections;


namespace EixoX.Web
{
    public static class HighCharts
    {

        public static void AppendDrilldown<T>(
            JavascriptBuilder builder,
            Tree<T> tree,
            AspectMember nameMember,
            AspectMember valueMember)
        {

            builder.AppendRaw("[");
            bool outercomma = false;
            foreach (TreeNode<T> node in tree)
            {
                if (outercomma)
                    builder.AppendRaw(", ");
                else
                    outercomma = true;

                builder.AppendLine("{");
                builder.AppendRaw("y: ");
                builder.Append(valueMember.GetValue(node.Value));
                builder.AppendLine(", drilldown: {");
                builder.AppendRaw("name: ");
                builder.Append(nameMember.GetValue(node.Value));
                builder.AppendLine(", categories: [");
                bool prependComma = false;
                foreach (TreeNode<T> child in node)
                {
                    if (prependComma)
                        builder.AppendRaw(", ");
                    else
                        prependComma = true;

                    builder.Append(nameMember.GetValue(node.Value));
                }
                builder.AppendLine("]");
                builder.AppendLine(", data: [");
                prependComma = false;
                foreach (TreeNode<T> child in node)
                {
                    if (prependComma)
                        builder.AppendRaw(", ");
                    else
                        prependComma = true;

                    builder.Append(valueMember.GetValue(node.Value));
                }
                builder.AppendLine("]");
                builder.AppendLine("}");
                builder.AppendRaw("}");
            }
            builder.AppendRaw("]");
                 
        }

        public static string DataDrilldown<T>(Tree<T> tree, string nameMember, string valueMember)
        {
            JavascriptBuilder builder = new JavascriptBuilder(512);
            AppendDrilldown<T>(
                builder,
                tree,
                ClassSchema<T>.Instance[nameMember],
                ClassSchema<T>.Instance[valueMember]);
            return builder.ToString();
        }

     
    }
}
