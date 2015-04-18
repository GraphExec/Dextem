using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Dextem
{
    public sealed class TypeParameterProcessor : BaseProcessor
    {
        public TypeParameterProcessor(ProcessorRegistry registry) : base(registry) { }

        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            if (context["lastNode"] != "typeparam")
            {
                writer.WriteLine("###### Type Parameters");
                writer.WriteLine("| Name | Description |");
                writer.WriteLine("| ---- | ----------- |");
            }

            string paramName = root.Attribute(XName.Get("name")).Value;
            if (context.ContainsKey(paramName))
            {
                writer.WriteLine("| *{0}* | {1} |",
                    context[paramName],
                    Regex.Replace(root.Value, "\\s+", " ", RegexOptions.Multiline));
            }
            else
            {
                writer.WriteLine("| {0} | {1} |",
                    paramName,
                    Regex.Replace(root.Value, "\\s+", " ", RegexOptions.Multiline));
            }

            context["lastNode"] = "typeparam";

            return base.Process(writer, root, context);
        }
    }
}
