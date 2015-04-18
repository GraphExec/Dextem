using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Dextem
{
    public sealed class ParameterProcessor : BaseProcessor
    {
        public ParameterProcessor(ProcessorRegistry registry) : base(registry) { }

        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            if (context["lastNode"] != "param")
            {
                writer.WriteLine("###### Parameter");
                writer.WriteLine("| Name | Description |");
                writer.WriteLine("| ---- | ----------- |");
            }

            string paramName = root.Attribute(XName.Get("name")).Value;
            if (context.ContainsKey(paramName))
            {
                writer.WriteLine("| {0} | *{1}*<br>{2} |",
                    paramName,
                    this.SanitizeParameterName(context[paramName]),
                    Regex.Replace(root.Value, "\\s+", " ", RegexOptions.Multiline));
            }
            else
            {
                writer.WriteLine("| {0} | *Unknown type*<br>{1} |",
                    paramName,
                    Regex.Replace(root.Value, "\\s+", " ", RegexOptions.Multiline));
            }

            context["lastNode"] = "param";

            return base.Process(writer, root, context);
        }

        private string SanitizeParameterName(string paramName)
        {
            if (paramName.Contains("{") || paramName.Contains("}"))
            {
                paramName = paramName.Replace("{", "").Replace("}", "");
            }
            else
            {
                paramName = paramName.Replace("&lt;", "").Replace("&gt;", "");
            }

            return paramName;
        }
    }
}
