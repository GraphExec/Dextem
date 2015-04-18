using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Dextem
{
    public sealed class ExceptionProcessor : BaseProcessor
    {
        public ExceptionProcessor(ProcessorRegistry registry) : base(registry) { }

        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            string exName = root.Attribute("cref").Value.Substring(2);
            exName = exName.Replace(context["assembly"] + ".", "");
            exName = exName.Replace(context["typeName"] + ".", "");
            writer.WriteLine("*{0}:* {1}\n",
                exName,
                Regex.Replace(root.Value, "\\s+", " ", RegexOptions.Multiline));

            return base.Process(writer, root, context);
        }
    }
}
