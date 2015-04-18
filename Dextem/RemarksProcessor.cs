using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Dextem
{
    public sealed class RemarksProcessor : BaseProcessor
    {
        public RemarksProcessor(ProcessorRegistry registry) : base(registry) { }

        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            writer.WriteLine("\n###### Remarks\n");
            writer.WriteLine("{0}\n", Regex.Replace(root.Value, "\\s+", " ", RegexOptions.Multiline));

            return base.Process(writer, root, context);
        }
    }
}
