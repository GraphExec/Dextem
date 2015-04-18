using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Dextem
{
    public sealed class AssemblyProcessor : BaseProcessor
    {
        public AssemblyProcessor(ProcessorRegistry registry) : base(registry) { }

        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            context[root.Name] = root.Element("name").Value;

            writer.WriteLine("\n# {0}\n", context[root.Name]);

            return base.Process(writer, root, context);
        }
    }
}
