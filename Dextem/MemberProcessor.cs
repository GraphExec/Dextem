using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Dextem
{
    public sealed class MemberProcessor : BaseProcessor
    {
        public MemberProcessor(ProcessorRegistry registry) : base(registry) { }

        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            var memberNameProcessor = this.Registry.Resolve("memberName");

            if (memberNameProcessor != null)
            {
                context = memberNameProcessor.Process(writer, root, context);
            }

            foreach (var node in root.Nodes().Where(x => x.NodeType == XmlNodeType.Element))
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
