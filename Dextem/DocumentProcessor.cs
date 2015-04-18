using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Dextem
{
    public sealed class DocumentProcessor : BaseProcessor
    {
        public DocumentProcessor(ProcessorRegistry registry) : base(registry) { }

        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            foreach (var node in root.Nodes())
            {
                var processor = this.Registry.Resolve(node.AsXElement().Name);

                if (processor != null)
                {
                    context = processor.Process(writer, node.AsXElement(), context);
                }
            }

            return base.Process(writer, root, context);
        }
    }
}
