using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Dextem
{
    public sealed class SummaryProcessor : BaseProcessor
    {
        public SummaryProcessor(ProcessorRegistry registry) : base(registry) { }

        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            string summary = Regex.Replace(root.Value, "\\s+", " ", RegexOptions.Multiline);
            writer.WriteLine("{0}\n", summary.Trim());

            return base.Process(writer, root, context);
        }
    }
}
